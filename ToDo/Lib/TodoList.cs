using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using ToDo.Lib.FormatPre04;

namespace ToDo.Lib
{
    [Serializable]
    public class ToDoList
    {
        public delegate void ListChangedEventHandler(object sender, EventArgs e);

        public ToDoList()
        {
            Changes = new List<Change>();
            Tasks = new List<Task>();
            ArchivedTasks = new List<Task>();
        }

        public List<Category> Categories { get; set; }

        public event ListChangedEventHandler ListChanged;

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

        public void ArchiveTask(int index)
        {
            if (ArchivedTasks == null)
            {
                ArchivedTasks = new List<Task>();
            }
            if (index >= Tasks.Count || index < 0)
            {
                return;
            }
            Task old = (Task)Tasks[index].Clone();
            Task t = (Task)old.Clone();
            t.ArchivedAt = DateTime.Now;
            ArchivedTasks.Add(t);
            Tasks.RemoveAt(index);
            AddChange(new Change(Environment.UserName, ChangeType.Archived, old, t));
        }

        public List<Task> ArchivedTasks
        {
            get;
            private set;
        }

        public void RemoveAtIndex(int index)
        {
            if (index >= Tasks.Count || index < 0)
            {
                return;
            }
            Task old = (Task)Tasks[index].Clone();
            Tasks.RemoveAt(index);
            AddChange(new Change(Environment.UserName, ChangeType.Delete, old, null));
        }

        public string[] ParseCategories()
        {
            List<string> result = new List<string>();
            foreach (Task t in Tasks)
            {
                if (!result.Contains(t.Category) && !string.IsNullOrEmpty(t.Category))
                {
                    result.Add(t.Category);
                }
            }
            return result.ToArray();
        }

        public void AddTask(Task t)
        {
            if (Tasks == null)
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
                    var bf = new BinaryFormatter();
                    object theDictionary = bf.Deserialize(str);
                    return (ToDoList)theDictionary;
                }
            }
            throw new FileNotFoundException("This todo list does not exist");
        }

        public static void Serialize(ToDoList theList, string path)
        {
            using (Stream str = new FileStream(path, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();

                //we have to tempsave and reset the event handler, because the mainform is linked in it. a form is not serializeable
                ListChangedEventHandler oldHandler = theList.ListChanged;
                theList.ListChanged = null;

                binaryFormatter.Serialize(str, theList);

                //setting the event handler to it's old state
                theList.ListChanged = oldHandler;
            }
        }

        public void AddCategory(Category cat)
        {
            Changes.Add(new Change(Environment.UserName, ChangeType.Add, null, cat.Clone()));
            Categories.Add(cat);
            OnListChanged(new object(), EventArgs.Empty);
        }

        public void UpdateOnlineVersion(string url)
        {
            var doc = new XmlDocument();
            doc.Load(url);
            Categories = new List<Category>();
            foreach (XmlNode main in doc.ChildNodes)
            {
                if (main.Name == "Version")
                {
                    OnlineVersion = Convert.ToInt64(main.InnerText);
                    return;
                }
            }
        }

        public int LocalVersion { get; private set; }

        public long OnlineVersion { get; private set; }
    }
}