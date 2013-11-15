using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Lib
{
    [Serializable]
    public class Change
    {
        public Change(string author, ChangeType type, object before, object after)
        {
            this.Type = type;
            this.Before = before;
            this.Author = author;
            this.After = after;
            this.Time = DateTime.Now;
        }

        public DateTime Time
        {
            get;
            private set;
        }

        public string Author
        {
            get;
            private set;
        }


        public ChangeType Type
        {
            get;
            private set;
        }

        public object Before
        {
            get;
            private set;
        }

        public object After
        {
            get;
            private set;
        }
    }
}
