using Microsoft.AspNetCore.Mvc;
using Back_End_Capstone_MS.Models;
using System.Collections.Generic;
using Back_End_Capstone_MS.Repositories;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Back_End_Capstone_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        public TagController(ITagRepository tagRepository) 
        {
            _tagRepository = tagRepository;
        }
        // GET: api/<TagController>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var tags = _tagRepository.GetAll();
            if (tags == null)
            {
                return NoContent();
            }
            return Ok(tags);
        }

        // GET api/<TagController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var tag = _tagRepository.GetById(id);
            if (tag == null)
            {
                return NoContent();
            }
            return Ok(tag);
        }

        // POST api/<TagController>
        [Authorize]
        [HttpPost]
        public IActionResult Post(Tag tag)
        {
            _tagRepository.Add(tag);
            return CreatedAtAction("Get", new { id = tag.Id }, tag);
        }

        // PUT api/<TagController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Tag tag)
        {
            if (id != tag.Id)
            {
                return BadRequest();
            }
            _tagRepository.Update(tag);
            return NoContent();
        }

        // DELETE api/<TagController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _tagRepository.Delete(id);
            return NoContent();
        }
    }
}
