using Domain.Utilites;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDapper
{
    public class SqlCommandProvider
    {
        private readonly SqlConnectionFactory _sqlConnectionFactory;
        private readonly SqlConnection _sqlConnection;
        public SqlCommandProvider (SqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        //public SqlCommandProvider (string ConnectionString)
        //{
        //    _sqlConnection = new SqlConnection(ConnectionString);
        //}

        //public SqlCommand Create(string sql , CommandType commandType)
        //{
        //    using (_sqlConnection)
        //    {
        //        var sqlCommand = _sqlConnection.CreateCommand();
        //        sqlCommand.CommandText = sql;
        //        sqlCommand.CommandType = commandType;
        //        return sqlCommand;
        //    }
        //}

        public SqlCommand Create(string Sql , CommandType commandType ,
            DynamicParameter parameters )
        {
            var sqlConnection = _sqlConnectionFactory.Create();
            sqlConnection.Open();
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = Sql;
            sqlCommand.CommandType = commandType;
            sqlCommand.Parameters.AddRange(parameters.ToSqlParameters().ToArray());
            return sqlCommand;
        }
        public SqlCommand Create(string Sql, CommandType CommandType)
        {
            var sqlConnection = _sqlConnectionFactory.Create();
            sqlConnection.Open();
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = Sql;
            sqlCommand.CommandType = CommandType;
            return sqlCommand;
        }
    }
}
