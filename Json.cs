using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JSON
{

    public static class Json
    {
        /// <summary>
        /// The JSON Value types
        /// </summary>
        public enum Type
        {
            Boolean = 0,
            String = 1,
            Integer = 2,
            Float = 3,
            Array = 4,
            Object = 5,
            Null = 6
        }

        /// <summary>
        /// The JSON Token types
        /// </summary>
        public enum Token
        {
            ObjectStart = 0,
            ObjectKey = 1,
            ObjectEnd = 2,
            ArrayStart = 3,
            ArrayEnd = 4,
            String = 5,
            Integer = 6,
            Float = 7,
            Boolean = 8,
            Null = 9,
            EndOfFile = 10
        }

        public static bool Validate(Token parent, Token next, out string error)
        {
            if (parent == Token.ObjectStart && ((next != Token.ObjectKey && next != Token.ObjectEnd) || next == Token.EndOfFile))
            {
                error = "Object expecting ObjectKey or ObjectEnd";
                return false;
            }
            else if (parent == Token.ArrayStart && (next == Token.ObjectEnd || next == Token.ObjectKey || next == Token.EndOfFile))
            {
                error = "Array expecting Value or ArrayEnd";
                return false;
            }
            else if (parent == Token.ObjectKey && (next == Token.ObjectEnd || next == Token.ArrayEnd || next == Token.EndOfFile))
            {
                error = "ObjectKey expecting Value";
                return false;
            }

            error = null;
            return true;
        }
    }

    public interface IStringHandler
    {
        string ReadString(string input);
        string WriteString(string input);
    }

    public class DefaultStringHandler : IStringHandler
    {
        public string ReadString(string input)
        {
            return input;
        }

        public string WriteString(string input)
        {
            return input;
        }
    }

    public class JsonIgnoreAttribute : Attribute { }
    public class JsonPathAttribute : Attribute
    {
        public string SerializedName;

        public JsonPathAttribute(string serializedName)
        {
            SerializedName = serializedName;
        }
    }
}
