using Domain.Utilites;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDapper
{
    public interface IAdoDapper<Entity>
        where Entity : class
    {
        public Task<List<Entity>> ExecuteQueryAsync(string query,
            DynamicParameter parameters = null, CommandType CommandType = CommandType.Text);
        public Task<Entity> ExecuteQuerySingleAsync(string query,
            DynamicParameter parameters = null, CommandType CommandType = CommandType.Text);
    }
}
