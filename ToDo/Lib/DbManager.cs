using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Lib
{
    public class DbManager
    {
        List<TodoList> todoLists;

        public DbManager()
        {
            todoLists = new List<TodoList>();

        }
    }
}
