using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ToDo.Lib
{
    [Serializable]
    public class ToDoList
    {
        public delegate void ListChangedEventHandler(object sender, EventArgs e);
        public event ListChangedEventHandler ListChanged;

        public ToDoList()
        {
            Changes = new List<Change>();
            Tasks = new List<Task>();
        }

        public void AddChangeWithoutEventTriggering(Change c)
        {
            Changes.Add(c);
            LocalVersion++;
        }

        public void ModifyTaskByIndex(int index, Task t)
        {
            Task old = (Task)Tasks[index].Clone();
            Tasks[index] = t;
            AddChange(new Change(Environment.UserName, ChangeType.Edit, old, t.Clone()));
        }

        public void RemoveAtIndex(int index)
        {
            Task old = (Task)Tasks[index].Clone();
            Tasks.RemoveAt(index);
            AddChange(new Change(Environment.UserName, ChangeType.Delete, old, null));
        }
        

        public string[] ParseCategories()
        {
            List<string> result = new List<string>();
            foreach(Task t in Tasks)
            {
               if(!result.Contains(t.Category) && !string.IsNullOrEmpty(t.Category))
               {
                   result.Add(t.Category);
               }
            }
            return result.ToArray();
        }

        public void AddTask(Task t)
        {
            if(Tasks == null)
            {
                Tasks = new List<Task>();
            }
            Tasks.Add(t);
            AddChange(new Change(Environment.UserName, ChangeType.Add, null, t.Clone()));
        }

        public List<Task> Tasks
        {
            get;
            private set;
        }
        public void AddChange(Change c)
        {
            Changes.Add(c);
            TriggerChangeEvent();
            LocalVersion++;
        }

        public void TriggerChangeEvent()
        {
            OnListChanged(new object(), new EventArgs());
        }

        private void OnListChanged(object sender, EventArgs e)
        {
            if (ListChanged != null)
            {
                ListChanged(sender, e);
                LocalVersion++;
            }
        }

        public List<Change> Changes
        {
            get;
            set;
        }

        public static ToDoList Deserialize(string path)
        {
            if (File.Exists(path))
            {
                using (Stream str = new FileStream(path, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                    var theDictionary = bf.Deserialize(str);
                    ToDoList tl = (ToDoList)theDictionary;
                    str.Close();
                    return tl;
                }
            }
            else
            {
                throw new FileNotFoundException("This todo list does not exist");
            }
        }

        public static void Serialize(ToDoList theList, string path)
        {
            using (Stream str = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();

                //we have to tempsave and reset the event handler, because the mainform is linked in it. a form is not serializeable
                ListChangedEventHandler oldHandler = theList.ListChanged;
                theList.ListChanged = null;

                bf.Serialize(str, theList);

                //setting the event handler to it's old state
                theList.ListChanged = oldHandler;

                str.Close();
            }
        }

        public long LocalVersion
        {
            get;
            private set;
        }

        public long OnlineVersion
        {
            get;
            private set;
        }
    }
}
