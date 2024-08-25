using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperService.Extension
{
    public static class DapperServices
    {   
        public static IServiceCollection AddDapperServices(this IServiceCollection Services
            ,IConfiguration Congiguration)
        {
            Services.AddScoped<IDbConnection>(provider =>
            new SqlConnection(Congiguration.GetConnectionString("DefaultConnection")));
            return Services;
        }
    }
}
