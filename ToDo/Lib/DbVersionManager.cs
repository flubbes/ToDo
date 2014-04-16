using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo.Lib
{
    public static class DbVersionManager
    {
        delegate object readerDelegate(string url);
        delegate void injectDelegate(string url);

        public static ToDoListVersion FindCorrectVersion(string path, out object resultTodoList)
        {
            resultTodoList = null;

            readerDelegate current = ToDoList.Deserialize;
            if (TryToRead(current, ref resultTodoList, path))
            {
                return ToDoListVersion.Current;
            }


            FormatPre04.TodoList oldTodoList = new FormatPre04.TodoList();
            injectDelegate xml = oldTodoList.FromXml;
            if (TryToRead(xml, ref resultTodoList, path))
            {
                return ToDoListVersion.Xml;
            }

            readerDelegate pre04 = FormatPre04.TodoList.DeserializeFromBinary;
            if (TryToRead(pre04, ref resultTodoList, path))
            {
                return ToDoListVersion.Pre04;
            }


            return ToDoListVersion.Unknown;
        }

        public static ToDoList ReadToDoList(string path)
        {
            object todoList = null;
            ToDoListVersion version = FindCorrectVersion(path, out todoList);
            switch (version)
            {
                case (ToDoListVersion.Current):
                    return (ToDoList)todoList;
                case (ToDoListVersion.Pre04):
                    return ConvertFromPre04((FormatPre04.TodoList)todoList);
                case (ToDoListVersion.Xml):
                    return ConvertFromPre04((FormatPre04.TodoList)todoList);
            }
            throw new Exception("Could not find correct database version");
        }

        private static ToDoList ConvertFromPre04(FormatPre04.TodoList list)
        {
            ToDoList result = new ToDoList();
            foreach (FormatPre04.Category cat in list.Categories)
            {
                foreach (FormatPre04.Task t in cat.Tasks)
                {
                    Task newTask = new Task();
                    newTask.Category = cat.Name;
                    newTask.SetIsDone(true);
                    newTask.DoneAt = t.DoneAt;
                    newTask.Text = t.Text;
                    result.AddTask(newTask);
                }
            }
            return result;
        }

        private static bool TryToRead(Delegate method, ref object result, params object[] args)
        {
            try
            {
                result = method.DynamicInvoke(args);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
