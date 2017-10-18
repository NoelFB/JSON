namespace JSON
{
    public class JsonFloat : IJsonValue
    {
        public Json.Type Type { get { return Json.Type.Float; } }
        public float Number;

        public JsonFloat() { }
        public JsonFloat(float number) { Number = number; }
    }
}
