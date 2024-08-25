using AdoDapper;
using Domain.Entities;
using Domain.Utilites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.IO.Pipelines;

namespace AdoVezeeta.Controllers
{
    [Route("api/v1/posts")]
    [ApiController]
    public class AdoDapperPostController : ControllerBase
    {
        private readonly IAdoDapper<Post> _adoDapper;
        public AdoDapperPostController(IAdoDapper<Post> adoDapper)
        {
            _adoDapper = adoDapper;
        }

        [HttpPost]
        public async Task<IResult> Create( string title,string content)
        {
            var sql = "INSERT INTO POSTS (Title, Content) VALUES (@Title, @Content)";
            var parameter = new DynamicParameter();
            parameter.Add("@Title", title); 
            parameter.Add("@Content", content);
            await _adoDapper.ExecuteQueryAsync(sql, parameter, CommandType.Text);
            return Results.Ok(new
            {
                message = "Created Successfully"
            });
        }

        [HttpGet]
        public async Task<IResult> GetAll()
        {
            var sql  = "SELECT * FROM POSTS";
            var posts = await _adoDapper.ExecuteQueryAsync(sql);
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
            var parameters = new DynamicParameter();
            parameters.Add("id", id);
            var post = await _adoDapper.ExecuteQuerySingleAsync(sql, parameters, CommandType.Text);
            return Results.Ok(new
            {
                message = "Fetched Successfully",
                Data = post
            });
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            var sql = "DELETE FROM POSTS WHERE id = @id";
            var parameters = new DynamicParameter();
            parameters.Add("id", id);
            await _adoDapper.ExecuteQueryAsync(sql, parameters, CommandType.Text);
            return Results.Ok(new
            {
                message = "Deleted Successfully"
            });
        }

        [HttpPatch("{id}")]
        public async Task<IResult> Update(int id, string title, string content)
        {
            var sql = "UPDATE POSTS SET Title = @Title, Content = @Content WHERE id = @id";
            var parameters = new DynamicParameter();
            parameters.Add("id", id);
            parameters.Add("Title", title);
            parameters.Add("Content", content);
            await _adoDapper.ExecuteQueryAsync(sql, parameters, CommandType.Text);
            return Results.Ok(new
            {
                message = "Updated Successfully"
            });
        }
    }
}
