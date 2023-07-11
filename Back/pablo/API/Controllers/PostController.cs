using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pablo.API.Data;
using pablo.API.Models.DTO;
using pablo.API.Models.Entities;

namespace pablo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly PabloDbContext dbContext;

        public PostController(PabloDbContext pabloDbContext)
        {
            this.dbContext = pabloDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await dbContext.Posts.ToListAsync();
            return Ok(posts);
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetPostById")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if(post !=null)
            {
                return Ok(post);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostRequest newPostRequest)
        {
            //DTO --> Entity
            var post = new Post()
            {
                Title = newPostRequest.Title,
                Content = newPostRequest.Content,
                Author = newPostRequest.Author,
                FeaturedImageUrl = newPostRequest.FeaturedImageUrl,
                PublishDate = newPostRequest.PublishDate,
                Summary = newPostRequest.Summary,
                UrlHandle = newPostRequest.UrlHandle,
                Visibility = newPostRequest.Visibility

            };
            post.Id = new Guid();
            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();

            //Return 201
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid id, UpdatePostRequest updatePostRequest)
        {
           
            //Checking if post exists
            var existingPost = await dbContext.Posts.FindAsync(id);
            if (existingPost != null)
            {

                existingPost.Title = updatePostRequest.Title;
                existingPost.Content = updatePostRequest.Content;
                existingPost.Author = updatePostRequest.Author;
                existingPost.FeaturedImageUrl = updatePostRequest.FeaturedImageUrl;
                existingPost.PublishDate = updatePostRequest.PublishDate;
               existingPost.Summary = updatePostRequest.Summary;
                existingPost.UrlHandle = updatePostRequest.UrlHandle;
                existingPost.Visibility = updatePostRequest.Visibility;

                await dbContext.SaveChangesAsync();
                return Ok(existingPost); 
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var existingPost = await dbContext.Posts.FindAsync(id);
            if(existingPost != null)
            {
                dbContext.Remove(existingPost);
                await dbContext.SaveChangesAsync();
                return Ok(existingPost);
            }
            return NotFound();
        }

    }
}
