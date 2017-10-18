using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON
{
    public class JsonBinaryWriter : IJsonWriter
    {

        public IStringHandler StringHandler = new DefaultStringHandler();
        private BinaryWriter writer;

        public JsonBinaryWriter(BinaryWriter writer)
        {
            this.writer = writer;
        }

        public JsonBinaryWriter(Stream stream)
        {
            writer = new BinaryWriter(stream);
        }

        public void ObjectStart()
        {
            writer.Write((byte)Json.Token.ObjectStart);
        }

        public void ObjectKey(string name)
        {
            writer.Write((byte)Json.Token.ObjectKey);
            writer.Write(StringHandler.WriteString(name));
        }

        public void ObjectEnd()
        {
            writer.Write((byte)Json.Token.ObjectEnd);
        }

        public void ArrayStart()
        {
            writer.Write((byte)Json.Token.ArrayStart);
        }

        public void ArrayEnd()
        {
            writer.Write((byte)Json.Token.ArrayEnd);
        }

        public void Value(string value)
        {
            if (value == null)
            {
                writer.Write((byte)Json.Token.Null);
            }
            else
            {
                writer.Write((byte)Json.Token.String);
                writer.Write(StringHandler.WriteString(value));
            }
        }

        public void Value(int value)
        {
            writer.Write((byte)Json.Token.Integer);
            writer.Write(value);
        }

        public void Value(float value)
        {
            writer.Write((byte)Json.Token.Float);
            writer.Write(value);
        }

        public void Value(bool value)
        {
            writer.Write((byte)Json.Token.Boolean);
            writer.Write(value);
        }

        public void Flush()
        {
            writer.Flush();
        }

        public void Dispose()
        {
            writer.Dispose();
        }

    }
}
