using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExtension
{
    public static class SqlCommandProvider
    {
        public static SqlCommand Create(
            string Sql,
            CommandType commandType,
            SqlConnection Connection )
        {
            var Command = Connection.CreateCommand();
            Command.CommandText = Sql;
            Command.CommandType = commandType;
            return Command;
        }
        public static SqlCommand Create(
            string Sql,
            CommandType commandType,
            DynamicParameter parameters,
            SqlConnection Connection )
        {
            var Command = Connection.CreateCommand();
            Command.CommandText = Sql;
            Command.CommandType = commandType;
            Command.Parameters.AddRange(parameters.ToSqlParameters().ToArray());
            return Command;
        }
    }
}
