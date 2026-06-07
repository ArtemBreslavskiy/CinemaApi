using System.ComponentModel.DataAnnotations;

namespace CinemaApi.DTOs
{
    public class CreateTicketDto
    {
        [Required]
        public int SessionId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public int SeatNumber { get; set; }
    }
}
