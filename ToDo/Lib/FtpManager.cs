using System;
using System.IO;
using System.Net;
using System.Threading;

namespace ToDo.Lib
{
    public class FtpManager
    {
        private bool _block;

        public FtpManager(string host, int port, string username, string password)
        {
            Host = host;
            Port = port;
            Username = username;
            Password = password;
        }

        public string CurrentActionText { get; private set; }

        public double CurrentActionProgress { get; private set; }

        public string Host { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }

        public void StartUpload(string filename)
        {
            new Thread(() => Upload(filename)) { IsBackground = true }.Start();
        }

        public void StartDownload(string filePath, string filename)
        {
            new Thread(() => Download(filePath, filename)) { IsBackground = true }.Start();
        }

        private void Upload(string filename)
        {
            if (_block)
            {
                return;
            }
            CurrentActionText = "Uploading " + filename;
            _block = true;
            var fileInf = new FileInfo(filename);
            var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + Host +
                                                                            "/" + fileInf.Name));
            reqFtp.Credentials = new NetworkCredential(Username, Password);
            reqFtp.KeepAlive = false;
            reqFtp.Method = WebRequestMethods.Ftp.UploadFile;
            reqFtp.UseBinary = true;
            reqFtp.ContentLength = fileInf.Length;
            const int buffLength = 2048;
            var buff = new byte[buffLength];
            FileStream fs = fileInf.OpenRead();
            Stream strm = reqFtp.GetRequestStream();
            long bytesRead = 0;
            int contentLen = fs.Read(buff, 0, buffLength);
            while (contentLen != 0)
            {
                bytesRead += contentLen;
                CurrentActionProgress = bytesRead * 100.0 / fs.Length;
                strm.Write(buff, 0, contentLen);
                contentLen = fs.Read(buff, 0, buffLength);
            }
            strm.Close();
            fs.Close();
            CurrentActionProgress = 100;
            _block = false;
        }

        private void Download(string filePath, string fileName)
        {
            if (_block)
            {
                return;
            }
            CurrentActionText = "Downloading " + fileName;
            _block = true;
            var outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

            var reqFtp = (FtpWebRequest)WebRequest.Create(new
                Uri("ftp://" + Host + "/" + fileName));
            reqFtp.KeepAlive = false;
            reqFtp.Method = WebRequestMethods.Ftp.DownloadFile;
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(Username, Password);
            var response = (FtpWebResponse)reqFtp.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            const int bufferSize = 2048;
            var buffer = new byte[bufferSize];
            long bytesRead = 0;
            if (ftpStream != null)
            {
                int readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    bytesRead += readCount;
                    CurrentActionProgress = bytesRead * 100.0 / ftpStream.Length;
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
            }
            outputStream.Close();
            response.Close();
            CurrentActionProgress = 100;
            _block = false;
        }
    }
}