using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Database
{
    public class SqlParameter
    {
        private readonly string parameterName;
        private readonly object value;

        public SqlParameter(string parameterName, object value)
        {
            this.parameterName = parameterName;
            this.value = value;
        }

        public string ParameterName => parameterName;
        public object Value => value;
    }
}
