using Microsoft.AspNetCore.Mvc;
using Back_End_Capstone_MS.Models;
using Back_End_Capstone_MS.Repositories;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Back_End_Capstone_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeLikeRepository _recipeLikeRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentLikeRepository _commentLikeRepository;
        public RecipeController(IRecipeRepository recipeRepository, IRecipeLikeRepository recipeLikeRepository,
            ICommentRepository commentRepository, ICommentLikeRepository commentLikeRepository)
        {
            _recipeRepository = recipeRepository;
            _recipeLikeRepository = recipeLikeRepository;
            _commentRepository = commentRepository;
            _commentLikeRepository = commentLikeRepository;
        }

        // GET: api/<RecipeController>
        //[Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var recipes = _recipeRepository.GetAll();
            foreach(Recipe r in recipes)
            {
                r.Comments = _commentRepository.GetByRecipeId(r.Id);
                r.RecipeLikes = _recipeLikeRepository.GetByRecipeId(r.Id);
            }
            if (recipes == null)
            {
                return NotFound();
            }
            return Ok(recipes);
        }

        //[Authorize]
        [HttpGet("userRecipes/{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            var recipes = _recipeRepository.GetByUserId(userId);
            
            if (recipes == null)
            {
                return NotFound();
            }
            return Ok(recipes);
        }

        // GET api/<RecipeController>/5
        //[Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var recipe = _recipeRepository.GetById(id);
            recipe.RecipeLikes = _recipeLikeRepository.GetByRecipeId(id);
            recipe.Comments = _commentRepository.GetByRecipeId(id);
            foreach(Comment c in recipe.Comments)
            {
                c.CommentLikes = _commentLikeRepository.GetByCommentId(c.Id);
            }
            if(recipe != null)
            {
                return Ok(recipe);
            }
            return NoContent();
        }

        // POST api/<RecipeController>
        [HttpPost]
        public IActionResult Post(Recipe recipe)
        {
            _recipeRepository.Create(recipe);
            return CreatedAtAction("Get", new { id = recipe.Id }, recipe);
        }

        // PUT api/<RecipeController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Recipe recipe)
        {
            if (recipe.Id != id)
            {
                return BadRequest();
            }
            _recipeRepository.Update(recipe);
            return NoContent();
        }

        // DELETE api/<RecipeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _recipeRepository.Delete(id);
            return NoContent();
        }
    }
}
