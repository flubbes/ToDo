using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Lib
{
    public class Settings
    {
        Dictionary<string, object> settings;

        public Settings()
        {
            settings = new Dictionary<string,object>();
        }

        public bool HasKey(string key)
        {
            return settings.Keys.Contains(key);
        }

        public T GetSetting<T>(string key)
        {
            object rawData;
            if (settings.TryGetValue(key, out rawData))
            {
                return (T)rawData;
            }
            else
            {
                throw new Exception("Could not get setting: " + key);
            }
        }

        public void StoreSetting(string key, object val)
        {
            if (HasKey(key))
            {
                settings.Remove(key);
            }
            settings.Add(key, val);
        }

        public void Deserialize(string path)
        {
            if (File.Exists(path))
            {
                using (Stream str = new FileStream(path, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    var theDictionary = bf.Deserialize(str);
                    settings = (Dictionary<string, object>)theDictionary;
                    str.Close();
                }
            }
            else
            {
                throw new Exception("Error while importing settings file: This file does not exists");
            }
        }

        public void Serialize(string path)
        {
            using (Stream str = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(str, settings);
                str.Close();
            }
        }
    }
}
