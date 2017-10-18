namespace JSON
{
    public class JsonInteger : IJsonValue
    {
        public Json.Type Type { get { return Json.Type.Integer; } }
        public int Number;

        public JsonInteger() { }
        public JsonInteger(int number) { Number = number; }
    }
}
