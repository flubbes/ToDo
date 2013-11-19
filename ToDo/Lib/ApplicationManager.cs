using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Reflection;

namespace ToDo.Lib
{
    public class ApplicationManager
    {
        public static void Initialize()
        {
            Updater = new Updater();
        }

        public static Updater Updater
        {
            get;
            private set;
        }

        public static string GetAppPath()
        {
            string result = Path.GetDirectoryName(Application.ExecutablePath);
            if(!result.EndsWith("/"))
            {
                result += "\\";
            }
            return result;
        }

        public static Icon GetAppIcon()
        {
            try
            {
                
                System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
                Stream st = a.GetManifestResourceStream("ToDo.ToDoLogo.ico");
                return new System.Drawing.Icon(st);
            }
            catch 
            {
                return null;
            }
        }
    }
}
