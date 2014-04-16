using System.Drawing;
﻿using System.IO;
﻿using System.Windows.Forms;

namespace ToDo.Lib
{
    public class ApplicationManager
    {
        public static Updater Updater { get; private set; }

        public static void Initialize()
        {
            Updater = new Updater();
        }

        public static string GetAppPath()
        {
            string result = Path.GetDirectoryName(Application.ExecutablePath);
            if (!result.EndsWith("/"))
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