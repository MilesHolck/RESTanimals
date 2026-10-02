using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RESTanimals.Models;
using RESTanimals.Repos;

namespace RESTanimals.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalsRepository _repo;

        // Dependency injection: repo'et kommer fra Program.cs
        public AnimalsController(IAnimalsRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Animals?nameStartsWith=F&minAge=2&sortOrder=age_desc
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public ActionResult<IEnumerable<Animal>> Get([FromQuery] string? nameStartsWith, [FromQuery] int? minAge, [FromQuery] string? sortOrder)
        {
            try
            {
                IEnumerable<Animal> animals = _repo.GetAnimals(nameStartsWith, minAge, sortOrder);
                if (animals.Any())
                {
                    return Ok(animals);
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Animals/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Animal> Get([FromRoute] int id)
        {
            Animal? animal = _repo.GetAnimalById(id);
            if (animal == null)
            {
                return NotFound("No such animal, id: " + id);
            }
            return Ok(animal);
        }

        // POST api/Animals
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Animal> Post([FromBody] Animal value)
        {
            try
            {
                Animal newAnimal = _repo.AddAnimal(value);
                return CreatedAtAction(nameof(Get), new { id = newAnimal.Id }, newAnimal);
            }
            catch (ArgumentException ex) // fanger også ArgumentNullException og ArgumentOutOfRangeException
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Animals/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<Animal> Put([FromRoute] int id, [FromBody] Animal value)
        {
            try
            {
                Animal? animal = _repo.UpdateAnimal(id, value);
                if (animal == null)
                {
                    return NotFound("No such animal, id: " + id);
                }
                return Ok(animal);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Animals/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Animal> Delete([FromRoute] int id)
        {
            Animal? animal = _repo.DeleteAnimal(id);
            if (animal == null)
            {
                return NotFound("No such animal, id: " + id);
            }
            return Ok(animal);
        }
    }
}
