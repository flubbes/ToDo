using System;

namespace ToDo.Lib
{
    [Serializable]
    public class Task : ICloneable
    {
        private bool _isDone;

        public string Text { get; set; }

        public DateTime DueDate
        {
            get;
            set;
        }

        public string EstimatedTime
        {
            get;
            set;
        }

        public DateTime ArchivedAt
        {
            get;
            set;
        }

        public void SetIsDone(bool newVal)
        {
            if (newVal)
            {
                DoneAt = DateTime.Now;
            }
            else
            {
                DoneAt = new DateTime();
            }
            object old = this.Clone();
            bool tempVal = _isDone;
            _isDone = newVal;
            if (_isDone != tempVal)
            {
                Change c = new Change(Environment.UserName, ChangeType.Edit, old, this.Clone());
                FormMain.ToDoList.AddChange(c);
            }
        }

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

        public string Category
        {
            get;
            set;
        }

        public int Priority
        {
            get;
            set;
        }

        public object Clone()
        {
            var t = new Task
            {
                DoneAt = DoneAt,
                IsDone = _isDone,
                Text = Text,
                DueDate = DueDate,
                EstimatedTime = EstimatedTime,
                Priority = Priority,
                Category = Category,
                ArchivedAt = ArchivedAt
            };
            return t;
        }
    }
}