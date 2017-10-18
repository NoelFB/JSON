using System.Collections;
using System.Collections.Generic;

namespace JSON
{
    public class JsonObject : IJsonValue, IEnumerable<KeyValuePair<string, IJsonValue>>
    {
        public Json.Type Type { get { return Json.Type.Object; } }
        public Dictionary<string, IJsonValue> Entries = new Dictionary<string, IJsonValue>();

        public int Count
        {
            get { return Entries.Count; }
        }

        public bool Has(string name)
        {
            return Entries.ContainsKey(name);
        }

        public IJsonValue Get(string name)
        {
            IJsonValue value;
            if (Entries.TryGetValue(name, out value))
                return value;
            return null;
        }

        public T Get<T>(string name) where T : IJsonValue
        {
            return (T)Get(name);
        }

        public IJsonValue this[string name]
        {
            get { return Get(name); }
        }

        public bool Bool(string name, bool defaultValue = false)
        {
            IJsonValue value;
            if (Entries.TryGetValue(name, out value))
            {
                var boolean = (value as JsonBool);
                if (boolean != null)
                    return boolean.Value;
            }
            return defaultValue;
        }

        public string String(string name, string defaultValue = null)
        {
            IJsonValue value;
            if (Entries.TryGetValue(name, out value))
            {
                var str = (value as JsonString);
                if (str != null)
                    return str.String;
            }

            return defaultValue;
        }

        public float Number(string name, float defaultValue = 0f)
        {
            IJsonValue value;
            if (Entries.TryGetValue(name, out value))
            {
                var num = (value as JsonFloat);
                if (num != null)
                    return num.Number;
            }

            return defaultValue;
        }

        public JsonArray Array(string name)
        {
            IJsonValue value;
            if (Entries.TryGetValue(name, out value))
                return (value as JsonArray);

            return null;
        }

        public JsonObject Object(string name)
        {
            IJsonValue value;
            if (Entries.TryGetValue(name, out value))
                return (value as JsonObject);

            return null;
        }

        public IJsonValue Add(string key, IJsonValue value)
        {
            Entries.Add(key, value);
            return value;
        }

        public JsonString Add(string key, string value)
        {
            var str = new JsonString(value);
            Entries.Add(key, str);
            return str;
        }

        public JsonFloat Add(string key, float value)
        {
            var num = new JsonFloat(value);
            Entries.Add(key, num);
            return num;
        }

        public JsonObject AddObject(string key)
        {
            var obj = new JsonObject();
            Entries.Add(key, obj);
            return obj;
        }

        public JsonArray AddArray(string key)
        {
            var arr = new JsonArray();
            Entries.Add(key, arr);
            return arr;
        }

        public bool Remove(string key)
        {
            return Entries.Remove(key);
        }

        // enumerate
        public IEnumerator<KeyValuePair<string, IJsonValue>> GetEnumerator() { return Entries.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
    }
}
