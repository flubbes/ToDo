using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Lib
{
    [Serializable]
    public class Category : ICloneable
    {
        public Category(string name)
        {
            Tasks = new List<Task>();
            this.Name = name;
        }

        public long ID
        {
            get;
            private set;
        }

        public List<Task> Tasks
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int TaskCount
        {
            get
            {
                return Tasks.Count;
            }
        }

        public int TasksDone
        {
            get
            {
                int result = 0;
                foreach (Task t in Tasks)
                {
                    if (t.IsDone)
                    {
                        result++;
                    }
                }
                return result;
            }
        }

        public void AddTask(Task t)
        {
            Tasks.Add(t);
        }

        public double CategoryPercentage
        {
            get
            {
                //if there are no tasks then this category is complete
                if(TaskCount <= 0)
                {
                    return 100;
                }
                try
                {
                    return TasksDone * 100 / TaskCount;
                }
                catch
                {
                    return 0.0;
                }
            }
        }

        public object Clone()
        {
            Category c = new Category(this.Name);
            c.Tasks = this.Tasks;
            c.ID = this.ID;
            return c;
        }
    }
}
