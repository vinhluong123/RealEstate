using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RealEstateServices.Models;

namespace RealEstateServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplimentController : ControllerBase
    {
        private readonly ILogger<ComplimentController> _logger;
        private readonly IMongoCollection<Compliment> _complimentCollection;

        public ComplimentController(
            ILogger<ComplimentController> logger,
            IMongoDatabase database)
        {
            _logger = logger;
            _complimentCollection = database.GetCollection<Compliment>("Compliments");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compliment>>> Get()
        {
            var compliments = await _complimentCollection.Find(_ => true).ToListAsync();
            return Ok(compliments);
        }

        [HttpGet("random")]
        public ActionResult<Compliment> GetRandomCompliment()
        {
            var compliment = _complimentCollection.AsQueryable().Sample(1).First();
            return Ok(compliment);
        }

        [HttpPost]
        public async Task<ActionResult<Compliment>> Post(Compliment compliment)
        {
            await _complimentCollection.InsertOneAsync(compliment);
            return CreatedAtAction("Get", new { id = compliment.Id }, compliment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Compliment compliment)
        {
            var retData = await _complimentCollection.FindOneAndUpdateAsync(
                t => t.Id == ObjectId.Parse(id),
                Builders<Compliment>.Update
                    .Set(t => t.Content, compliment.Content));

            if (retData == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _complimentCollection.DeleteOneAsync(t => t.Id == ObjectId.Parse(id));
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
