using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToDo.Lib
{
    public class FtpManager
    {
        Thread ftpThread;
        bool block;

        public FtpManager(string host, int port, string username, string password)
        {
            Host = host;
            Port = port;
            Username = username;
            Password = password;
        }

        public string CurrentActionText
        {
            get;
            private set;
        }

        public double CurrentActionProgress
        {
            get;
            private set;
        }

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
            if (block)
            {
                return;
            }
            CurrentActionText = "Uploading " + filename;
            block = true;
            FileInfo fileInf = new FileInfo(filename);
            string uri = "ftp://" +
            Host + "/" + fileInf.Name;
            FtpWebRequest reqFTP;
            reqFTP =
            (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + Host +
            "/" + fileInf.Name));
            reqFTP.Credentials = new NetworkCredential(Username, Password);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            Stream strm = reqFTP.GetRequestStream();
            long bytesRead = 0;
            contentLen = fs.Read(buff, 0, buffLength);
            while (contentLen != 0)
            {
                bytesRead += contentLen;
                CurrentActionProgress = bytesRead * 100 / fs.Length;
                strm.Write(buff, 0, contentLen);
                contentLen = fs.Read(buff, 0, buffLength);
                
            }
            strm.Close();
            fs.Close();
            CurrentActionProgress = 100;
            block = false;
        }

        private void Download(string filePath, string fileName)
        {
            if (block)
            {
                return;
            }
            CurrentActionText = "Downloading " + fileName;
            block = true;
            FtpWebRequest reqFTP;
            FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new
            Uri("ftp://" + Host + "/" + fileName));
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
            reqFTP.UseBinary = true;
            reqFTP.Credentials = new NetworkCredential(Username, Password);
            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            long cl = response.ContentLength;
            int bufferSize = 2048;
            int readCount;
            byte[] buffer = new byte[bufferSize];
            long bytesRead = 0;
            readCount = ftpStream.Read(buffer, 0, bufferSize);
            while (readCount > 0)
            {
                bytesRead += readCount;
                CurrentActionProgress = bytesRead * 100 / ftpStream.Length;
                outputStream.Write(buffer, 0, readCount);
                readCount = ftpStream.Read(buffer, 0, bufferSize);
            }
            ftpStream.Close();
            outputStream.Close();
            response.Close();
            CurrentActionProgress = 100;
            block = false;
        }

        public string Host
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }
    }
}
