using AdoDapper;
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
        private IAdoDapper<Post> _adoDapper;
        private readonly SqlConnectionFactory _sqlConnectionFactoy= new SqlConnectionFactory(ConnectionString);
        private readonly ApplicationDbContext _dbContext;
        public Benchmark()
        {
            var sqlCommandProvider = new SqlCommandProvider(_sqlConnectionFactoy);
            var reflectionReader = new ReflectionReader<Post>();
            _adoDapper = new AdoDapper<Post>(sqlCommandProvider, reflectionReader);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer(ConnectionString)
                    .Options;
            _dbContext = new ApplicationDbContext(options);
        }

        [Benchmark]
        public async Task DapperGetAll()
        {
            var connection = _sqlConnectionFactoy.Create();
            using (connection)
            {
                var result = await connection.QueryAsync<Post>("SELECT * FROM POSTS");
            }
        }

        [Benchmark]
        public async Task AdoGetAll()
        {
            var sql = "SELECT * FROM POSTS";
            var posts = await _adoDapper.ExecuteQueryAsync(sql);
        }

        [Benchmark]
        public async Task EFGetAll()
        {
            var posts = await _dbContext.Posts.ToListAsync();
        }
    }
}

