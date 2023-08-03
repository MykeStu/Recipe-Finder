using Microsoft.AspNetCore.Mvc;
using Back_End_Capstone_MS.Models;
using Back_End_Capstone_MS.Repositories;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Back_End_Capstone_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeTagController : ControllerBase
    {
        private readonly IRecipeTagRepository _recipeTagRepository;
        public RecipeTagController(IRecipeTagRepository recipeTagRepository)
        {
            _recipeTagRepository = recipeTagRepository;
        }

        // POST api/<RecipeTagController>
        [Authorize]
        [HttpPost]
        public IActionResult Post(RecipeTag rt)
        {
            _recipeTagRepository.Add(rt);
            return CreatedAtAction("Get", new { id = rt.Id }, rt);
        }

        // DELETE api/<RecipeTagController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _recipeTagRepository.Delete(id);
            return NoContent();
        }
    }
}
