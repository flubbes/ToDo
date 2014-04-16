using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Lib.FormatPre04;

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