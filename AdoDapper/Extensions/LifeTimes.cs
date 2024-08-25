using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDapper.Extensions
{
    public static class LifeTimes
    {
        public static IServiceCollection AddLifeTimes(this IServiceCollection Services
             , string connectionString )
        {
            Services.AddSingleton(options =>
            {
                //var ConnectionString = configuration.GetConnectionString(Section) 
                //?? throw new ArgumentNullException("Connection String not found");

                var sqlConnectionFactory  = new SqlConnectionFactory(connectionString);
                return sqlConnectionFactory;
            });
            Services.AddScoped(typeof(IAdoDapper<>), typeof(AdoDapper<>));
            Services.AddScoped(typeof(ReflectionReader<>));
            Services.AddScoped<SqlCommandProvider>();
            return Services;
        }
    }
}
