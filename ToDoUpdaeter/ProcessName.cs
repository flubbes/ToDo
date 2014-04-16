using System;
using System.Linq;

namespace ToDoUpdater
{
    internal static class ProcessName
    {
        private const char Splitter = '.';

        internal static string Extract(string fileName)
        {
            if (IsValidFileName(fileName))
            {
                return SplitAndGetProcessName(fileName);
            }
            throw new ArgumentException();
        }

        private static string SplitAndGetProcessName(string fileName)
        {
            return fileName.Split('.').First();
        }

        private static bool IsValidFileName(string fileName)
        {
            return fileName.Contains(Splitter);
        }
    }
}