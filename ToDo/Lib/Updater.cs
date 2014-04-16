using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace ToDo.Lib
{
    public class Updater
    {
        private readonly DateTime _lastCheck;

        public Updater()
        {
            TryDeleteUpdater();
            _lastCheck = DateTime.Now.Subtract(TimeSpan.FromDays(1));
        }

        /// <summary>
        ///     The local version
        /// </summary>
        internal long LocalVersion
        {
            get { return 6; }
        }

        public long OnlineVersion { get; private set; }

        private static void TryDeleteUpdater()
        {
            if (File.Exists("TodoUpdater.exe"))
            {
                File.Delete("TodoUpdater.exe");
            }
        }

        private void RefreshOnlineVersion()
        {
            try
            {
                OnlineVersion =
                    Convert.ToInt64(new WebClient().DownloadString("http://todo-update.kabesoft.de/version.php"));
            }
            catch
            {
                throw new Exception("Could not connect to update server");
            }
        }

        public void DownloadUpdate()
        {
            var cl = new WebClient();
            cl.DownloadFileCompleted += cl_DownloadFileCompleted;
            if (File.Exists("temp"))
            {
                File.Delete("temp");
            }
            cl.DownloadFileAsync(new Uri("http://todo-update.kabesoft.de/ToDo.exe"), "temp");
        }

        private void cl_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var stream = GetType().Assembly.GetManifestResourceStream("ToDo.ToDoUpdater.exe");
            if (stream != null)
            {
                var bytes = new byte[(int)stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                File.WriteAllBytes("ToDoUpdater.exe", bytes);
            }
            Process.Start("ToDoUpdater.exe", "ToDo.exe temp");
            Application.Exit();
        }

        public bool HasUpdate()
        {
            if ((DateTime.Now - _lastCheck).TotalSeconds <= 30)
            {
                return false;
            }
            RefreshOnlineVersion();
            return LocalVersion < OnlineVersion;
        }
    }
}