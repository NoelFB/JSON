namespace JSON
{
    public class JsonBool : IJsonValue
    {
        public Json.Type Type { get { return Json.Type.Boolean; } }
        public bool Value;

        public JsonBool() { }
        public JsonBool(bool value) { Value = value; }
    }
}
