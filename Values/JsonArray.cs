using System.Collections.Generic;

namespace JSON
{
    public class JsonArray : List<IJsonValue>, IJsonValue
    {
        public Json.Type Type { get { return Json.Type.Array; } }

        public JsonString Add(string value)
        {
            var str = new JsonString(value);
            Add(str);
            return str;
        }

        public JsonFloat Add(float value)
        {
            var num = new JsonFloat(value);
            Add(num);
            return num;
        }

        public JsonObject AddObject()
        {
            var obj = new JsonObject();
            Add(obj);
            return obj;
        }

        public JsonArray AddArray()
        {
            var arr = new JsonArray();
            Add(arr);
            return arr;
        }
    }
}
