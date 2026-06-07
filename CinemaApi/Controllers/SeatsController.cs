using CinemaApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SeatsController : ControllerBase
    {
        private readonly CinemaDbContext _db;

        public SeatsController(CinemaDbContext db)
        {
            _db = db;
        }

        [HttpGet("{Id}")]
        public IActionResult GetFreeSeatsNumbers(int Id)
        {
            var session = _db.Sessions.FirstOrDefault(s => s.Id == Id);
            if (session == null)
                return BadRequest("Session does not exist");

            var hall = _db.Halls.FirstOrDefault(h => h.Id == session.HallId);

            HashSet<int> seats = new(Enumerable.Range(1, hall.SeatsCount));

            foreach (var ticket in _db.Tickets.Where(t => t.SessionId == Id))
            {
                seats.Remove(ticket.SeatNumber);
            }

            return Ok(seats);
        }
    }
}
