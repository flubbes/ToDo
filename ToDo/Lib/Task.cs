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

        public bool IsDone
        {
            get
            {
                return isDone;
            }
            set
            {
                if (value)
                {
                    DoneAt = DateTime.Now;
                }
                else
                {
                    DoneAt = new DateTime();
                }
                isDone = value;
            }
        }

        public DateTime DoneAt
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
            return t;
        }
    }
}
