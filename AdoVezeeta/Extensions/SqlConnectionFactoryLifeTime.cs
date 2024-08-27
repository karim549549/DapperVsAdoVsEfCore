namespace Ado_Dapper_efcore.Extensions
{
    public static class SqlConnectionFactoryLifeTime
    {
        public static IServiceCollection AddSqlConnectionFactory(
            this IServiceCollection Services , string ConnectionString)
        {
            Services.AddSingleton(options =>{
                return new SqlConnectionFactory(ConnectionString);
            });
            return Services;
        }
    }
}
