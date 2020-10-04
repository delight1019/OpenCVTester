namespace OpenCVTester.Common
{
    public class StringValue: System.Attribute
    {
        private readonly string _value;

        public string Value
        {
            get { return _value; }
        }

        public StringValue(string value)
        {
            _value = value;
        }        
    }
}
