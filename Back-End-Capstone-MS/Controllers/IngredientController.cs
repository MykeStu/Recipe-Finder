using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Back_End_Capstone_MS.Models;
using Back_End_Capstone_MS.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Back_End_Capstone_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepository;
        public IngredientController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }
        // GET: api/<IngredientController>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var ingredients = _ingredientRepository.GetAll();
            if (ingredients == null)
            {
                return NotFound();
            }
            return Ok(ingredients);
        }

        // POST api/<IngredientController>
        [Authorize]
        [HttpPost]
        public IActionResult Post(Ingredient ingredient)
        {
            _ingredientRepository.Add(ingredient);
            return CreatedAtAction("Get", new { id = ingredient.Id }, ingredient);
        }

        // DELETE api/<IngredientController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _ingredientRepository.Delete(id);
            return NoContent();
        }
        [Authorize]
        [HttpGet("search")]
        public IActionResult Search(string searchParameters)
        {
            var ingredients = _ingredientRepository.Search(searchParameters);
            if(ingredients == null) { return NotFound(); }
            return Ok(ingredients);
        }
    }
}
