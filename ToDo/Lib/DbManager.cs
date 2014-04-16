using System.Collections.Generic;

namespace ToDo.Lib
{
    public class DbManager
    {
        public DbManager()
        {
            TodoLists = new List<TodoList>();
        }

        public List<TodoList> TodoLists { get; set; }
    }
}