using CinemaApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly CinemaDbContext _db;

        public SessionsController(CinemaDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetSessions()
        {
            return Ok(_db.Sessions.ToList());
        }
    }
}