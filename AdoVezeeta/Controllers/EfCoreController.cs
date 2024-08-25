using Domain.Entities;
using EfCoreSerivce;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdoVezeeta.Controllers
{
    [Route("api/v3/posts")]
    [ApiController]
    public class EfCoreController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Post> posts;
        public EfCoreController(ApplicationDbContext context)
        {
            _context = context;
            posts = _context.Set<Post>();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new
            {
                message = "Fetched Successfully",
                Data = await posts.ToListAsync()
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            return Ok(new
            {
                message = "Fetched Successfully",
                Data = await posts.FindAsync(id)
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                message = "Deleted Successfully"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, string content)
        {
            var post = new Post
            {
                title = title,
                content = content
            };
            posts.Add(post);
            await _context.SaveChangesAsync();
            return Ok( new 
            {
                message = "Created Successfully"
            });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, string title, string content)
        {
            var post = await posts.FindAsync(id);
            post.title = title;
            post.content = content;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                message= "Updated Successfully"
            });
        }
    }
}
