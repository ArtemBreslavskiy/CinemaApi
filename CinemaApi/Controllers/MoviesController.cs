using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetMovies([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            int skip = (page - 1) * pageSize;

            var movies = await _db.Movies
                .OrderBy(m => m.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return Ok(movies);
        }
    }
}
