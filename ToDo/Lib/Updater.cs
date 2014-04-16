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
<<<<<<< HEAD
        private readonly DateTime _lastCheck;
=======
        DateTime lastCheck;
        string updaterPath, newTodoPath, toDoPath;


>>>>>>> origin/0.4-alpha

        public Updater()
        {

            updaterPath = ApplicationManager.GetAppPath() + "ToDoUpdater.exe";
            newTodoPath = ApplicationManager.GetAppPath() + "ToDo_new.exe";
            toDoPath = ApplicationManager.GetAppPath() + "ToDo.exe";
            TryDeleteUpdater();
<<<<<<< HEAD
            _lastCheck = DateTime.Now.Subtract(TimeSpan.FromDays(1));
=======
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
>>>>>>> origin/0.4-alpha
        }

        /// <summary>
        ///     The local version
        /// </summary>
        internal long LocalVersion
        {
<<<<<<< HEAD
            get { return 6; }
=======
            get
            {
                return 5;
            }
>>>>>>> origin/0.4-alpha
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
<<<<<<< HEAD
            if (File.Exists("temp"))
            {
                File.Delete("temp");
            }
            cl.DownloadFileAsync(new Uri("http://todo-update.kabesoft.de/ToDo.exe"), "temp");
=======
            if(File.Exists(newTodoPath))
            {
                File.Delete(newTodoPath);
            }
            cl.DownloadFileAsync(new Uri("http://todo-update.kabesoft.de/ToDo.exe"), newTodoPath);
>>>>>>> origin/0.4-alpha
        }

        private void cl_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
<<<<<<< HEAD
            var stream = GetType().Assembly.GetManifestResourceStream("ToDo.ToDoUpdater.exe");
            if (stream != null)
            {
                var bytes = new byte[(int)stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                File.WriteAllBytes("ToDoUpdater.exe", bytes);
            }
            Process.Start("ToDoUpdater.exe", "ToDo.exe temp");
=======
            Stream stream = GetType().Assembly.GetManifestResourceStream("ToDo.ToDoUpdater.exe");
            byte[] bytes = new byte[(int)stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            File.WriteAllBytes(updaterPath, bytes);


            Process.Start(updaterPath);
>>>>>>> origin/0.4-alpha
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
