using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Lib.FormatPre04
{
    public class TodoListChangedEventArgs : EventArgs
    {
        public TodoListChangedEventArgs(Change change)
        {
            Change = change;
        }

        public Change Change { get; private set; }
    }
}