using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo.Lib
{
    public class Updater
    {
        DateTime lastCheck;

        public Updater()
        {
            TryDeleteUpdater();
            lastCheck = DateTime.Now.Subtract(TimeSpan.FromDays(1));
        }

        private void TryDeleteUpdater()
        {
            try
            {
                if (File.Exists("TodoUpdater.exe"))
                {
                    File.Delete("TodoUpdater.exe");
                }
            }
            catch { }
        }

        /// <summary>
        /// The local version
        /// </summary>
        internal long LocalVersion
        {
            get
            {
                return 0;
            }
        }

        public long OnlineVersion
        {
            get;
            private set;
        }

        private void RefreshOnlineVersion()
        {
            try
            {
                OnlineVersion = Convert.ToInt64(new WebClient().DownloadString("http://todo-update.kabesoft.de/version.php"));
            }
            catch
            {
                throw new Exception("Could not connect to update server");
            }
        }

        public void DownloadUpdate()
        {
            WebClient cl = new WebClient();
            cl.DownloadFileCompleted += cl_DownloadFileCompleted;
            if(File.Exists("ToDo_new.exe"))
            {
                File.Delete("Todo_new.exe");
            }
            cl.DownloadFileAsync(new Uri("http://todo-update.kabesoft.de/ToDo.exe"), "ToDo_new.exe");
        }

        void cl_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Stream stream = GetType().Assembly.GetManifestResourceStream("ToDo.ToDoUpdater.exe");
            byte[] bytes = new byte[(int)stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            File.WriteAllBytes("ToDoUpdater.exe", bytes);


            Process.Start("ToDoUpdater.exe");
            Application.Exit();
        }

        public bool HasUpdate()
        {
            if((DateTime.Now - lastCheck).TotalSeconds <= 30)
            {
                return false;
            }
            RefreshOnlineVersion();
            if(LocalVersion < OnlineVersion)
            {
                return true;
            }
            return false;
        }
    }
}
