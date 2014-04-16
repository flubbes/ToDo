using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Lib.FormatPre04
{
    [Serializable]
    public class Category : ICloneable
    {
        public Category(string name)
        {
            Tasks = new List<Task>();
            Name = name;
        }

        public long Id { get; private set; }

        public List<Task> Tasks { get; set; }

        public string Name { get; set; }

        public int TaskCount
        {
            get { return Tasks.Count; }
        }

        public int TasksDone
        {
            get
            {
                return Tasks.Count(t => t.IsDone);
            }
        }

        public double CategoryPercentage
        {
            get
            {
                //if there are no tasks then this category is complete
                if (TaskCount <= 0)
                {
                    return 100;
                }
                try
                {
                    return TasksDone * 100.0 / TaskCount;
                }
                catch
                {
                    return 0.0;
                }
            }
        }

        public object Clone()
        {
            var c = new Category(Name) { Tasks = Tasks, Id = Id };
            return c;
        }

        public void AddTask(Task t)
        {
            Tasks.Add(t);
        }
    }
}