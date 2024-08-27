using Dapper;
using DapperExtension.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Ado_Dapper_efcore.Controllers
{
    [Route("api/v4/posts")]
    [ApiController]
    public class newDapperController : ControllerBase
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public newDapperController(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        [HttpGet]
        public async Task<IResult> GetAll()
        {
            var Connection = _connectionFactory.Create();
            return Results.Ok(new
            {
                message = "fetched successfully",
                Data = await Connection.QueryAsync<Post>("SELECT * FROM POSTS")
            });
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetById(int id)
        {
            var Connection = _connectionFactory.Create();
            return Results.Ok(new
            {
                message = "fetched successfully",
                Data = await Connection.QuerySingleAsync<Post>("SELECT * FROM POSTS WHERE id = @id", new { id = id })
            });
        }

        [HttpPost]
        public async Task<IResult> Create(string title, string content)
        {
            var Connection = _connectionFactory.Create();
            var sql = "INSERT INTO POSTS (Title, Content) VALUES (@Title, @Content)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", title);
            parameters.Add("@Content", content);
            await Connection.QueryAsync(sql, parameters);
            return Results.Ok(new
            {
                message = "Created Successfully"
            });
        }
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            var Connection = _connectionFactory.Create();
            var sql = "DELETE FROM POSTS WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            await Connection.QueryAsync(sql, parameters);
            return Results.Ok(new
            {
                message = "Deleted Successfully"
            });
        }
        [HttpPatch("{id}")]
        public async Task<IResult> Update(int id, string title, string content)
        {
            var Connection = _connectionFactory.Create();
            var sql = "UPDATE POSTS SET Title = @Title, Content = @Content WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@Title", title);
            parameters.Add("@Content", content);
            await Connection.QueryAsync(sql, parameters);
            return Results.Ok(new
            {
                message = "Updated Successfully"
            });
        }
    }
}
