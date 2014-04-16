<<<<<<< HEAD
﻿using System.Collections.Generic;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Lib.FormatPre04;
>>>>>>> origin/0.4-alpha

namespace ToDo.Lib
{
    public class DbManager
    {
<<<<<<< HEAD
        public DbManager()
        {
            TodoLists = new List<TodoList>();
=======
        List<ToDoList> toDoLists;

        public DbManager()
        {
            toDoLists = new List<ToDoList>();

>>>>>>> origin/0.4-alpha
        }

        public List<TodoList> TodoLists { get; set; }
    }
}
