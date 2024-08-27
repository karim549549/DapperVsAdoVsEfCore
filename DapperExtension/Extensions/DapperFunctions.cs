using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExtension.Extensions
{
    public static class DapperFunctions
    {
        public async static Task<List<Entity>> QueryAsync<Entity>(
            this SqlConnection Connection,
            string Sql,
            CommandType commandType = CommandType.Text
        ) where Entity : class
        {
            using (Connection)
            {
                Connection.Open();
                var Command = SqlCommandProvider.Create(Sql, commandType, Connection);
                using (var reader = await Command.ExecuteReaderAsync())
                {
                    var list = new List<Entity>();
                    while (await reader.ReadAsync())
                    {
                      var entity= ReflectionReader.ReadEntityFromReader<Entity>(reader);
                      list.Add(entity);
                    }
                    return list;
                }
            }
        }
        public static async Task<Entity> QuerySingleOrDefaultAsync<Entity>(
            this SqlConnection Connection,
            string Sql,
            DynamicParameter parameters,
            CommandType commandType = CommandType.Text
            )
            where Entity : class
        {
            using (Connection)
            {
                var Command = SqlCommandProvider.Create(
                    Sql,
                    commandType,
                    parameters,
                    Connection);
                using (var reader =await Command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        return ReflectionReader.ReadEntityFromReader<Entity>(reader);
                    }
                    return default;
                }
            }
        }
    }
}
