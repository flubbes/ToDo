using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ToDo.Lib
{
    public class CategoryManager
    {
        static Thread thrWorker;
        public delegate void ListChangedEventHandler(object sender, EventArgs e);
        public static event ListChangedEventHandler ListChanged;

        public static void Init()
        {
            Categories = new List<Category>();
            thrWorker = new Thread(WorkerMethod);
            thrWorker.IsBackground = true;
        }

        private static void WorkerMethod()
        {
            while (true)
            {
                Thread.Sleep(1);
            }
        }

        public static void OnListChanged(object sender, EventArgs e)
        {
            if (ListChanged != null)
            {
                ListChanged(sender, e);
                LocalVersion++;
            }
        }

        public static List<Category> Categories
        {
            get;
            set;
        }

        public static void AddCategory(Category cat)
        {
            Categories.Add(cat);
            OnListChanged(new object(), new EventArgs());
        }

        public static long LocalVersion
        {
            get;
            private set;
        }

        public static long OnlineVersion
        {
            get;
            private set;
        }

        public static void UpdateOnlineVersion(string url)
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

        public static void FromXml(string url)
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

        public static void ToXml(string path)
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
