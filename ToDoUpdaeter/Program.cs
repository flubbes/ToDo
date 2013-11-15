using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoUpdaeter
{
    static class Program
    {
        static void Main()
        {
            DateTime lastMessage = DateTime.Now;
            while(Process.GetProcessesByName("ToDo").Count() >= 1)
            {
                if((DateTime.Now - lastMessage).TotalSeconds >= 20)
                {
                    MessageBox.Show("Please close ALL instances of ToDo! There is is at least one instance running");
                    lastMessage = DateTime.Now;
                }
                Thread.Sleep(1);
            }
            if(File.Exists("ToDo_new.exe"))
            {
                if(File.Exists("ToDo.exe"))
                {
                    File.Delete("ToDo.exe");
                }
                File.Copy("ToDo_new.exe", "ToDo.exe");
                File.Delete("ToDo_new.exe");
            }
            if (File.Exists("ToDo.exe"))
            {
                MessageBox.Show("Update finished");
                Process.Start("ToDo.exe");
            }
        }
    }
}
