namespace CinemaApi.Models;

public class Ticket
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public string CustomerName { get; set; } = "";
    public int SeatNumber { get; set; }
}
