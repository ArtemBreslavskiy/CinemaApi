using CinemaApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly CinemaDbContext _db;

        public MoviesController(CinemaDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            return Ok(_db.Movies.ToList());
        }
    }
}
