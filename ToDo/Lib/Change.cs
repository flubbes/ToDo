using System;

namespace ToDo.Lib
{
    [Serializable]
    public class Change
    {
        public Change(string author, ChangeType type, object before, object after)
        {
            Type = type;
            Before = before;
            Author = author;
            After = after;
            Time = DateTime.Now;
        }

        public DateTime Time { get; private set; }

        public string Author { get; private set; }

        public ChangeType Type { get; private set; }

        public object Before { get; private set; }

        public object After { get; private set; }
    }
}