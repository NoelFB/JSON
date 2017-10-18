using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON
{
    
    public interface IJsonReader : IDisposable
    {

        Json.Token Read();
        Json.Token CurrentToken { get; }
        object CurrentValue { get; }

    }

    public static class JsonReader
    {
        public static JsonObject Parse(this IJsonReader reader)
        {
            return reader.ParseObject();
        }

        private static JsonObject ParseObject(this IJsonReader reader)
        {
            var obj = new JsonObject();
            while (reader.Read() != Json.Token.EndOfFile)
            {
                if (reader.CurrentToken == Json.Token.ObjectStart)
                    continue;
                if (reader.CurrentToken == Json.Token.ObjectKey)
                {
                    var key = (string)reader.CurrentValue;
                    var value = reader.ParseValue();
                    obj.Add(key, value);
                }
                else if (reader.CurrentToken == Json.Token.ObjectEnd)
                    break;
            }
            return obj;
        }

        private static IJsonValue ParseValue(this IJsonReader reader)
        {
            reader.Read();

            if (reader.CurrentToken == Json.Token.String)
            {
                return new JsonString((string)reader.CurrentValue);
            }
            else if (reader.CurrentToken == Json.Token.Integer)
            {
                return new JsonFloat((int)reader.CurrentValue);
            }
            else if (reader.CurrentToken == Json.Token.Float)
            {
                return new JsonFloat((float)reader.CurrentValue);
            }
            else if (reader.CurrentToken == Json.Token.Boolean)
            {
                return new JsonBool((bool)reader.CurrentValue);
            }
            else if (reader.CurrentToken == Json.Token.ArrayStart)
            {
                var array = new JsonArray();
                while (reader.CurrentToken != Json.Token.EndOfFile)
                {
                    var value = reader.ParseValue();
                    if (reader.CurrentToken == Json.Token.ArrayEnd)
                        break;
                    array.Add(value);
                }
                return array;
            }
            else if (reader.CurrentToken == Json.Token.ObjectStart)
            {
                return reader.ParseObject();
            }

            return null;
        }

        public static T Deserialize<T>(this IJsonReader reader)
        {
            var instance = Activator.CreateInstance<T>();
            return instance;
        }
    }
}
