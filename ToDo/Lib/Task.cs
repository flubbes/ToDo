using System;

namespace ToDo.Lib
{
    [Serializable]
    public class Task : ICloneable
    {
        private bool _isDone;

        public string Text { get; set; }

        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                DoneAt = value ? DateTime.Now : new DateTime();
                _isDone = value;
            }
        }

        public DateTime DoneAt { get; set; }

        public object Clone()
        {
            var t = new Task { DoneAt = DoneAt, IsDone = _isDone, Text = Text };
            return t;
        }
    }
}