using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ToDo.Lib
{
    public static class ApplicationManager
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
    }
}
