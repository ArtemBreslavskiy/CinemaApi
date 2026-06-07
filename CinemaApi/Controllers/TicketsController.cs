using CinemaApi.Data;
using CinemaApi.DTOs;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly CinemaDbContext _db;

        public TicketsController(CinemaDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetTickets()
        {
            return Ok(_db.Tickets.ToList());
        }

        [HttpPost]
        public IActionResult BuyTicket(CreateTicketDto dto)
        {
            var sessionExist = _db.Sessions.Any(s => s.Id == dto.SessionId);
            if (!sessionExist)
                return BadRequest("Session does not exist");

            var isSeatTaken = _db.Tickets.Any(t => t.SessionId == dto.SessionId
            && t.SeatNumber == dto.SeatNumber);
            if (isSeatTaken)
                return BadRequest("This seat is already occupied");

            Ticket ticket = new()
            {
                SessionId = dto.SessionId,
                CustomerName = dto.CustomerName,
                SeatNumber = dto.SeatNumber,
            };

            _db.Add(ticket);
            _db.SaveChanges();

            return Created("", ticket);
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteTicket(int Id)
        {
            var ticketToDelete = _db.Tickets.FirstOrDefault(t => t.Id == Id);
            if (ticketToDelete == null)
                return BadRequest("Ticket does not exist");

            _db.Remove(ticketToDelete);
            _db.SaveChanges();

            return NoContent();
        }
    }
}

