using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Lib
{
    public class TodoListChangedEventArgs : EventArgs
    {
        public TodoListChangedEventArgs(Change change) : base()
        {
            this.Change = change;
        }

        public Change Change
        {
            get;
            private set;
        }
    }
}
