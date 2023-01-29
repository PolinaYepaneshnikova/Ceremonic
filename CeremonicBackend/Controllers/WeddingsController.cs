using CeremonicBackend.DB.NoSQL;
using CeremonicBackend.DB.Relational;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CeremonicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeddingsController : ControllerBase
    {
        private ICeremonicMongoDbContext _mongoDB { get; set; }
        public WeddingsController(ICeremonicMongoDbContext db)
        {
            _mongoDB = db;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var weddings = _mongoDB.Weddings.Find(w => true).ToList();

            return Ok(weddings);
        }
    }
}
