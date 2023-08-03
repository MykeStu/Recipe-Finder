using Microsoft.AspNetCore.Mvc;
using Back_End_Capstone_MS.Models;
using Back_End_Capstone_MS.Repositories;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Back_End_Capstone_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentLikeRepository _commentLikeRepository;
        public CommentController(ICommentRepository commentRepository, ICommentLikeRepository commentLikeRepository)
        {
            _commentRepository = commentRepository;
            _commentLikeRepository = commentLikeRepository;
        }

        // GET api/<CommentController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var comment = _commentRepository.GetById(id);
            comment.CommentLikes = _commentLikeRepository.GetByCommentId(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }
        // POST api/<CommentController>
        [Authorize]
        [HttpPost]
        public IActionResult Post(Comment comment)
        {
            _commentRepository.AddComment(comment);
            return CreatedAtAction("Get", new {id = comment.Id}, comment);
        }

        // PUT api/<CommentController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Comment comment)
        {
            if (comment.Id != id)
            {
                return BadRequest();
            }
            _commentRepository.UpdateComment(comment);
            return NoContent();
        }
        // DELETE api/<CommentController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _commentRepository.DeleteComment(id);
            return NoContent();
        }
    }
}
