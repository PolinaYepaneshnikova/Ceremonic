using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Xml.Linq;

using CeremonicBackend.DB.Relational;

namespace CeremonicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private CeremonicRelationalDbContext _relationalDB { get; set; }
        public UsersController(CeremonicRelationalDbContext db)
        {
            _relationalDB = db;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _relationalDB.Users;

            return Ok(users);
        }
    }
}
