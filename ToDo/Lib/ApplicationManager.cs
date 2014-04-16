namespace ToDo.Lib
{
    public static class ApplicationManager
    {
        public static Updater Updater { get; private set; }

        public static void Initialize()
        {
            Updater = new Updater();
        }
    }
}