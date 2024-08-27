using Ado_Dapper_efcore;
using BenchmarkDotNet.Attributes;
using Dapper;
using Domain.Entities;
using EfCoreSerivce;
using Microsoft.Data.SqlClient;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace AdoVezeeta.BenchMarkers
{
    [MemoryDiagnoser]
    public class Benchmark
    {
        private const string ConnectionString = "Server=DESKTOP-L6DHQ4D;Database=AdoDapperTask;Trusted_Connection=True;TrustServerCertificate=True;Max Pool Size=100000;Min Pool Size=1;";

        private readonly SqlConnectionFactory _sqlConnectionFactoy= new SqlConnectionFactory(ConnectionString);
        private readonly ApplicationDbContext _dbContext;
        private SqlConnection _connection;
        public Benchmark()
        {
            _connection = _sqlConnectionFactoy.Create();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer(ConnectionString)
                    .Options;
            _dbContext = new ApplicationDbContext(options);
        }

        [Benchmark]
        public async Task DapperGetAll()
        {

            await _connection.QueryAsync<Post>("SELECT * FROM POSTS");
            
        }
        [Benchmark]
        public async Task NewDapper()
        {
            await _connection.QueryAsync<Post>("SELECT * FROM POSTS");
        }
        [Benchmark]
        public async Task EFGetAll()
        {
            var posts = await _dbContext.Posts.ToListAsync();
        }

    }
}

