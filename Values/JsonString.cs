namespace JSON
{
    public class JsonString : IJsonValue
    {
        public Json.Type Type { get { return Json.Type.String; } }
        public string String;

        public JsonString() { }
        public JsonString(string text) { String = text; }
    }
}
