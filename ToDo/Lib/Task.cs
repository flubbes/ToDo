using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Lib
{
    [Serializable]
    public class Task : ICloneable
    {
        private bool isDone;
        public string Text
        {
            get;
            set;
        }

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
            bool tempVal = isDone;
            isDone = newVal;
            if (isDone != tempVal)
            {
                Change c = new Change(Environment.UserName, ChangeType.Edit, old, this.Clone());
                FormMain.ToDoList.AddChange(c);
            }
        }

        public bool IsDone
        {
            get
            {
                return isDone;
            }
            protected set
            {
                isDone = value;
            }
        }

        public DateTime DoneAt
        {
            get;
            set;
        }

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
            Task t = new Task();
            t.DoneAt = this.DoneAt;
            t.IsDone = this.isDone;
            t.Text = this.Text;
            t.DueDate = this.DueDate;
            t.EstimatedTime = this.EstimatedTime;
            t.Priority = this.Priority;
            t.Category = this.Category;
            t.ArchivedAt = this.ArchivedAt;
            return t;
        }
    }
}
