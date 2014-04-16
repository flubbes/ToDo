using System;
<<<<<<< HEAD:ToDo/Lib/TodoListChangedEventArgs.cs
=======
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
>>>>>>> origin/0.4-alpha:ToDo/Lib/FormatPre04/TodoListChangedEventArgs.cs

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
