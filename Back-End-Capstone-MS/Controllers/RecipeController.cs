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
        private readonly IRecipeTagRepository _recipeTagRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly ICommentLikeRepository _commentLikeRepository;
        public RecipeController(IRecipeRepository recipeRepository, IRecipeLikeRepository recipeLikeRepository, IRecipeTagRepository recipeTagRepository
            , ICommentRepository commentRepository, IRecipeIngredientRepository recipeIngredientRepository, ICommentLikeRepository commentLikeRepository)
        {
            _recipeRepository = recipeRepository;
            _recipeLikeRepository = recipeLikeRepository;
            _recipeTagRepository = recipeTagRepository;
            _commentRepository = commentRepository;
            _recipeTagRepository = recipeTagRepository;
            _recipeIngredientRepository = recipeIngredientRepository;
            _commentLikeRepository = commentLikeRepository;
        }

        // GET: api/<RecipeController>
        //[Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var recipes = _recipeRepository.GetAllPublic();
            foreach (var recipe in recipes)
            {
                recipe.Comments = _commentRepository.GetByRecipeId(recipe.Id);
                recipe.RecipeLikes = _recipeLikeRepository.GetByRecipeId(recipe.Id);
                recipe.RecipeTags = _recipeTagRepository.GetByRecipeId(recipe.Id);
                recipe.RecipeIngredients = _recipeIngredientRepository.GetByRecipeId(recipe.Id);
            }
            if (recipes == null)
            {
                return NotFound();
            }
            return Ok(recipes);
        }

        // GET api/<RecipeController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var recipe = _recipeRepository.GetById(id);
            recipe.RecipeTags = _recipeTagRepository.GetByRecipeId(id);
            recipe.RecipeLikes = _recipeLikeRepository.GetByRecipeId(id);
            recipe.Comments = _commentRepository.GetByRecipeId(id);
            recipe.RecipeIngredients = _recipeIngredientRepository.GetByRecipeId(id);
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
