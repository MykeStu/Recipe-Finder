using Microsoft.AspNetCore.Mvc;
using Back_End_Capstone_MS.Repositories;
using Back_End_Capstone_MS.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        [HttpGet("DoesUserExist/{FireBaseUserId}")]
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
        public IActionResult Get(int id)
        {
            var user = _userRepository.GetById(id);
            if(user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        // POST api/<UserController>
        [Authorize]
        [HttpPost]
        public IActionResult Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            _userRepository.Add(user);
            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        // PUT api/<UserController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            _userRepository.Update(user);
            return NoContent();
        }
        [Authorize]
        [HttpGet("Me")]
        public IActionResult Me()
        {
            var user = GetCurrentUser();
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        private User GetCurrentUser()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userRepository.GetByFireBaseUserId(firebaseUserId);
        }
    }
}
