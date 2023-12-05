namespace WpfApp.Core
{
    public class SqlParameter
    {
        private readonly string _name;
        private readonly object _value; 

        public SqlParameter(string parameterName, object value)
        {
            this._name = parameterName;
            this._value = value;
        }

        public string Name => _name;
        public object Value => _value;
    }
}