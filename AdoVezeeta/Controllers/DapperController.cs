using Dapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AdoVezeeta.Controllers
{
    [Route("api/v2/posts")]
    [ApiController]
    public class DapperController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;
        public DapperController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        [HttpPost]
        public async Task<IActionResult> Create(string title, string content)
        {
            var sql = "INSERT INTO POSTS (Title, Content) VALUES (@Title, @Content)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", title);
            parameters.Add("@Content", content);
            var result  =await _dbConnection.ExecuteAsync(sql,
                parameters,commandType: CommandType.Text);
            return Ok(new
            {
                message = "Created Successfully",
                data= result
            });
        }
        [HttpGet]
        public async  Task<IResult> GetAll()
        {
            var sql = "SELECT * FROM POSTS";
            var posts = await _dbConnection.QueryAsync<Post>(sql);
            return Results.Ok(new
            {
                message = "Fetched Successfully",
                Data = posts
            });
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetById(int id)
        {
            var sql = "SELECT * FROM POSTS  WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            var post = await _dbConnection.QuerySingleAsync<Post>(sql, parameters, commandType: CommandType.Text);
            return Results.Ok(new
            {
                message = "Fetched Successfully",
                Data = post
            });
        }
        [HttpPatch("{id}")]
        public async Task<IResult> Update(int id, string title, string content)
        {
            var sql = "UPDATE POSTS SET Title = @Title, Content = @Content WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            parameters.Add("Title", title);
            parameters.Add("Content", content);
            await _dbConnection.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            return Results.Ok(new
            {
                message = "Updated Successfully"
            });
        }
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            var sql = "DELETE FROM POSTS WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            await _dbConnection.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            return Results.Ok(new
            {
                message = "Deleted Successfully"
            });
        }
    }
}
