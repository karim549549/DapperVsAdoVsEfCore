using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreSerivce.Extensions
{
    public static class EfExtensions
    {
        public static IServiceCollection AddEfCoreServices(this IServiceCollection Serivces ,
            IConfiguration configuration)
        {
            Serivces.AddDbContext<ApplicationDbContext>(options =>
                  options.UseSqlServer(
                      configuration.GetConnectionString("DefaultConnection")??
                      throw new Exception("Connection string is null"),
                      sqlServerOptions => sqlServerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                  )
              );
            return Serivces;
        }
    }
}
