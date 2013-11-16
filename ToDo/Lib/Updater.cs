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
        string updaterPath, newTodoPath, toDoPath;

        public Updater()
        {
            updaterPath = ApplicationManager.GetAppPath() + "ToDoUpdater.exe";
            newTodoPath = ApplicationManager.GetAppPath() + "ToDo_new.exe";
            toDoPath = ApplicationManager.GetAppPath() + "ToDo.exe";
            TryDeleteUpdater();
            lastCheck = DateTime.Now.Subtract(TimeSpan.FromDays(1));
           
        }

        private void TryDeleteUpdater()
        {
            try
            {
                if (File.Exists(updaterPath))
                {
                    File.Delete(updaterPath);
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
                return 3;
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
            if(File.Exists(newTodoPath))
            {
                File.Delete(newTodoPath);
            }
            cl.DownloadFileAsync(new Uri("http://todo-update.kabesoft.de/ToDo.exe"), newTodoPath);
        }

        void cl_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Stream stream = GetType().Assembly.GetManifestResourceStream("ToDo.ToDoUpdater.exe");
            byte[] bytes = new byte[(int)stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            File.WriteAllBytes(updaterPath, bytes);


            Process.Start(updaterPath);
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
