using Domain.Utilites;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AdoDapper
{
    public class AdoDapper<Entity> : IAdoDapper<Entity>
        where Entity : class
    {
        private readonly SqlCommandProvider _sqlCommandProvider;
        private readonly ReflectionReader<Entity> _reflectionReader;
        public AdoDapper(SqlCommandProvider sqlCommandProvider,
            ReflectionReader<Entity> reflectionReader)
        {
            _sqlCommandProvider = sqlCommandProvider;
            _reflectionReader = reflectionReader;
        }
        public async Task<List<Entity>> ExecuteQueryAsync(string query,
            DynamicParameter parameters = null , CommandType CommandType = CommandType.Text)
        {
            var Command =new SqlCommand();
            if (parameters is not null)
            {
                 Command = _sqlCommandProvider.Create(query, CommandType, parameters);
            }
            else
            {
                 Command = _sqlCommandProvider.Create(query, CommandType);
            }
            var reader = Command.ExecuteReader();
            var result = await _reflectionReader.ReadListAsync(reader);
            return result;
        }

        public async Task<Entity> ExecuteQuerySingleAsync(string query,
            DynamicParameter parameters = null, CommandType CommandType = CommandType.Text)
        {
            var Command = new SqlCommand();
            if (parameters is not null)
            {
                Command = _sqlCommandProvider.Create(query, CommandType, parameters);
            }
            else
            {
                Command = _sqlCommandProvider.Create(query, CommandType);
            }
            var reader  = Command.ExecuteReader();
            var result = await _reflectionReader.ReadSingleAsync(reader);
            return result;
        }
    }
}
