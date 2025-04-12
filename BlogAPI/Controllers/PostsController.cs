using DBManager.DbModels;
using DBManager.IServices;
using DBManager.Pagging.PaggingClasses;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/Posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            try
            {
                var post=await _postService.GetPostAsync(id);
                return Ok(post);
            }
            catch
            {
                return NotFound();
            }
        }


        [HttpPost]
        public async Task<ActionResult<Post>> AddPost([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _postService.AddPostAsync(post);
                return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<PaggingPost>> GetAllPosts(int pageid=1,int pagesize=5,string content="",string title="",string author="")
        {
            var result = await _postService.DoPaggingPostsAsync(pagesize, pageid, title, content, author);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> UpdatePost(int id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != post.Id)
            {
                return BadRequest();
            }
            if(!await _postService.IsExistPostAsync(id))
            {
                return NotFound();
            }
            var result = await _postService.UpdatePostAsync(post);
            return Ok(result);

        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if(!await _postService.IsExistPostAsync(id))
            {
                return NotFound();
            }
            await _postService.DeletePostAsync(id);
            return Ok(new { Message = "Post Deleted" });
        }
    }
}
