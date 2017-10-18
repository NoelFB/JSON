using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON
{
    public class JsonBinaryReader : IJsonReader
    {
        public IStringHandler StringHandler = new DefaultStringHandler();

        private BinaryReader reader;
        private Json.Token token;
        private object value;

        public Json.Token CurrentToken { get { return token; } }
        public object CurrentValue { get { return value; } }
        
        public JsonBinaryReader(BinaryReader reader)
        {
            this.reader = reader;
        }

        public JsonBinaryReader(Stream stream)
        {
            reader = new BinaryReader(stream);
        }

        public JsonBinaryReader(byte[] buffer)
        {
            reader = new BinaryReader(new MemoryStream(buffer));
        }

        public Json.Token Read()
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return token = Json.Token.EndOfFile;

            token = (Json.Token)reader.ReadByte();
            if (token == Json.Token.ObjectKey || token == Json.Token.String)
                value = StringHandler.ReadString(reader.ReadString());
            else if (token == Json.Token.Float)
                value = reader.ReadSingle();
            else if (token == Json.Token.Integer)
                value = reader.ReadInt32();
            else if (token == Json.Token.Boolean)
                value = reader.ReadBoolean();
            else if (token == Json.Token.Null)
                value = null;
            return token;
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
