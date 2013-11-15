using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
