using Microsoft.AspNetCore.Mvc;
using Back_End_Capstone_MS.Repositories;
using Back_End_Capstone_MS.Models;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Back_End_Capstone_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/<UserController>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userRepository.GetAll();
            if (users != null)
            {
            return Ok(users);
            }
            return NotFound();
        }

        [Authorize]
        [HttpGet("FireBase/{FireBaseUserId}")]
        public IActionResult Get(string FireBaseUserId)
        {
            var user = _userRepository.GetByFireBaseUserId(FireBaseUserId);
            if(user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        // GET api/<UserController>/5
        [Authorize]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [Authorize]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
