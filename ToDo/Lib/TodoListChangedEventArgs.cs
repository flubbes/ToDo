using System;

namespace ToDo.Lib
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