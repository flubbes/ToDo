using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;

namespace ToDo.Lib
{
    [Serializable]
    public class TodoList
    {
        public delegate void ListChangedEventHandler(object sender, EventArgs e);

        public TodoList()
        {
            Categories = new List<Category>();
            Changes = new List<Change>();
        }

        public List<Category> Categories { get; set; }

        public List<Change> Changes { get; set; }

        public long LocalVersion { get; private set; }

        public long OnlineVersion { get; private set; }

        public event ListChangedEventHandler ListChanged;

        public void AddChangeWithoutEventTriggering(Change c)
        {
            Changes.Add(c);
            LocalVersion++;
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

        public static TodoList DeserializeFromBinary(string path)
        {
            if (File.Exists(path))
            {
                using (Stream str = new FileStream(path, FileMode.Open))
                {
                    var bf = new BinaryFormatter();
                    object theDictionary = bf.Deserialize(str);
                    var tl = (TodoList)theDictionary;
                    return tl;
                }
            }
            throw new FileNotFoundException("This todo list does not exist");
        }

        public static void SerializeToBinary(ref TodoList theList, string path)
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

        public void FromXml(string url)
        {
            if (!File.Exists(url))
            {
                return;
            }
            var doc = new XmlDocument();
            doc.Load(url);
            Categories = new List<Category>();
            foreach (XmlNode main in doc.ChildNodes)
            {
                if (main.Name == "ToDoDb")
                {
                    if (main.Attributes != null)
                    {
                        foreach (XmlAttribute verAttr in main.Attributes)
                        {
                            if (verAttr.Name == "Version")
                            {
                                LocalVersion = Convert.ToInt64(verAttr.InnerText);
                            }
                        }
                        foreach (XmlNode dN in main.ChildNodes)
                        {
                            if (dN.Name == "Categories")
                            {
                                foreach (XmlNode cN in dN.ChildNodes)
                                {
                                    if (cN.Name == "Category")
                                    {
                                        var c = new Category("DUMMY");
                                        if (cN.Attributes != null)
                                        {
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
                                                            var t = new Task();
                                                            if (tN.Attributes != null)
                                                            {
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
        }

        public void ToXml(string path)
        {
            var xtw = new XmlTextWriter(path, Encoding.UTF8) { Formatting = Formatting.Indented };
            xtw.WriteStartDocument();
            xtw.WriteStartElement("ToDoDb");
            xtw.WriteAttributeString("Version", LocalVersion.ToString(CultureInfo.InvariantCulture));

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
                    xtw.WriteAttributeString("DoneAt", t.DoneAt.ToString(CultureInfo.InvariantCulture));
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