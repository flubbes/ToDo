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
    public class TodoList
    {
        public delegate void ListChangedEventHandler(object sender, TodoListChangedEventArgs e);
        public event ListChangedEventHandler ListChanged;

        public TodoList()
        {
            Categories = new List<Category>();
            Changes = new List<Change>();
        }

        public void OnListChanged(object sender, TodoListChangedEventArgs e)
        {
            if (ListChanged != null)
            {
                Changes.Add(e.Change);
                ListChanged(sender, e);
                LocalVersion++;
            }
        }

        public List<Category> Categories
        {
            get;
            set;
        }

        public List<Change> Changes
        {
            get;
            set;
        }

        public static TodoList DeserializeFromBinary(string path)
        {
            if (File.Exists(path))
            {
                using (Stream str = new FileStream(path, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    var theDictionary = bf.Deserialize(str);
                    TodoList tl = (TodoList)theDictionary;
                    str.Close();
                    return tl;
                }


            }
            else
            {
                throw new FileNotFoundException("This todo list does not exist");
            }
        }

        public static void SerializeToBinary(ref TodoList theList, string path)
        {
            using (Stream str = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();

                //we have to reset the event handler, because the mainform is linked in it. a form is not serializeable
                theList.ListChanged = null;
                bf.Serialize(str, theList);
                str.Close();
            }
        }

        public void AddCategory(Category cat)
        {
            Change c = new Change(Environment.UserName, ChangeType.Add, null, cat.Clone());
            Categories.Add(cat);
            OnListChanged(new object(), new TodoListChangedEventArgs(c));
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

        public void UpdateOnlineVersion(string url)
        {
            XmlDocument doc = new XmlDocument();
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

        public void FromXml(string url)
        {
            if (!File.Exists(url))
            {
                return;
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(url);
            Categories = new List<Category>();
            foreach (XmlNode main in doc.ChildNodes)
            {
                if (main.Name == "ToDoDb")
                {
                    foreach (XmlAttribute verAttr in main.Attributes)
                    {
                        if(verAttr.Name == "Version")
                        {
                            LocalVersion = Convert.ToInt64(verAttr.InnerText);
                        }
                    }
                    foreach(XmlNode dN in main.ChildNodes)
                    {
                        if (dN.Name == "Categories")
                        {
                            foreach (XmlNode cN in dN.ChildNodes)
                            {
                                if (cN.Name == "Category")
                                {
                                    Category c = new Category("DUMMY");
                                    foreach (XmlAttribute cA in cN.Attributes)
                                    {
                                        if (cA.Name == "Name")
                                        {
                                            c.Name = cA.InnerText;
                                        }                                        
                                    }
                                    foreach (XmlNode taskNode in cN.ChildNodes)
                                    {
                                        if (taskNode.Name == "Tasks")
                                        {
                                            foreach (XmlNode tN in taskNode.ChildNodes)
                                            {
                                                if (tN.Name == "Task")
                                                {
                                                    Task t = new Task();
                                                    foreach (XmlAttribute tA in tN.Attributes)
                                                    {
                                                        if (tA.Name == "Text")
                                                        {
                                                            t.Text = tA.InnerText;
                                                        }
                                                        else if (tA.Name == "IsDone")
                                                        {
                                                            t.IsDone = Convert.ToBoolean(tA.InnerText);
                                                        }
                                                        else if (tA.Name == "DoneAt")
                                                        {
                                                            t.DoneAt = Convert.ToDateTime(tA.InnerText);
                                                        }
                                                    }
                                                    c.Tasks.Add(t);
                                                }
                                            }
                                        }
                                    }
                                    Categories.Add(c);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ToXml(string path)
        {
            XmlTextWriter xtw = new XmlTextWriter(path, UnicodeEncoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            xtw.WriteStartDocument();
            xtw.WriteStartElement("ToDoDb");
            xtw.WriteAttributeString("Version", LocalVersion.ToString());

            xtw.WriteStartElement("Categories");
            foreach (Category c in Categories)
            {
                xtw.WriteStartElement("Category");
                xtw.WriteAttributeString("Name", c.Name);
                xtw.WriteStartElement("Tasks");
                foreach (Task t in c.Tasks)
                {
                    xtw.WriteStartElement("Task");
                    xtw.WriteAttributeString("Text", t.Text);
                    xtw.WriteAttributeString("IsDone", t.IsDone.ToString());
                    xtw.WriteAttributeString("DoneAt", t.DoneAt.ToString());
                    xtw.WriteEndElement();
                }
                xtw.WriteEndElement();
                xtw.WriteEndElement();
            }
            xtw.WriteEndElement();
            xtw.WriteEndElement();
            xtw.WriteEndDocument();
            xtw.Flush();
            xtw.Close();
        }
    }
}
