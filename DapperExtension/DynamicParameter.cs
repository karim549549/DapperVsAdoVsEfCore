using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExtension
{
    public class DynamicParameter
    {
        public IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        public void Add(string key, object value)
        {
            Parameters.Add(key, value);
        }
        public List<SqlParameter> ToSqlParameters()
        {
            var SqlParameterCollection = new List<SqlParameter>();
            foreach (var param in Parameters)
            {
                var sqlParam = new SqlParameter(param.Key, param.Value ?? DBNull.Value);
                SqlParameterCollection.Add(sqlParam);
            }
            return SqlParameterCollection;
        }
    }
}
