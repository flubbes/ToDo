using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace ToDoUpdater
{
    public static class Updater
    {
        private static void WaitForExit(string processName)
        {
            while (IsProcessRunning(processName))
            {
                SleepMilliseconds(1);
            }
        }

        private static void SleepMilliseconds(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        internal static void DoUpdate(string exeName, string updateFileName)
        {
            WaitForExit(exeName);
            TryDeleteFile(exeName);
            CopyAndDeleteFile(updateFileName, exeName);
            TryStartProcess(exeName);
        }

        private static void CopyAndDeleteFile(string tempFileName, string exeName)
        {
            File.Copy(tempFileName, exeName);
            File.Delete(tempFileName);
        }

        private static void TryDeleteFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        private static void TryStartProcess(string fileName)
        {
            if (File.Exists(fileName))
            {
                Process.Start(fileName);
            }
        }

        private static bool IsProcessRunning(string processName)
        {
            return Process.GetProcessesByName(ProcessName.Extract(processName)).Any();
        }
    }
}