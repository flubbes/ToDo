using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace ToDo.Lib
{
    public class Settings
    {
        private Dictionary<string, object> _settings;

        public Settings()
        {
            _settings = new Dictionary<string, object>();
        }

        public bool HasKey(string key)
        {
            return _settings.Keys.Contains(key);
        }

        public T GetSetting<T>(string key)
        {
            object rawData;
            if (_settings.TryGetValue(key, out rawData))
            {
                return (T)rawData;
            }
            throw new Exception("Could not get setting: " + key);
        }

        public void StoreSetting(string key, object val)
        {
            if (HasKey(key))
            {
                _settings.Remove(key);
            }
            _settings.Add(key, val);
        }

        public void Deserialize(string path)
        {
            if (File.Exists(path))
            {
                using (Stream str = new FileStream(path, FileMode.Open))
                {
                    var bf = new BinaryFormatter();
                    object theDictionary = bf.Deserialize(str);
                    _settings = (Dictionary<string, object>)theDictionary;
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
                var bf = new BinaryFormatter();
                bf.Serialize(str, _settings);
            }
        }
    }
}