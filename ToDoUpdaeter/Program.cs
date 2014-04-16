using System;
using System.Linq;

namespace ToDoUpdater
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Count() >= 2)
            {
                Updater.DoUpdate(args[0], args[1]);
            }
        }
    }
}