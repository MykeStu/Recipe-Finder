using Microsoft.AspNetCore.Mvc;
using Back_End_Capstone_MS.Models;
using Back_End_Capstone_MS.Repositories;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Back_End_Capstone_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeIngredientController : ControllerBase
    {
        private readonly IRecipeIngredientRepository _RIRepo;
        public RecipeIngredientController(IRecipeIngredientRepository riRepo)
        {
            _RIRepo = riRepo;
        }

        // POST api/<RecipeIngredientController>
        [Authorize]
        [HttpPost]
        public IActionResult Post(RecipeIngredient ri)
        {
            _RIRepo.Add(ri);
            return CreatedAtAction("Get", new { id = ri.Id }, ri);
        }

        // DELETE api/<RecipeIngredientController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _RIRepo.Delete(id);
            return NoContent();
        }
    }
}
