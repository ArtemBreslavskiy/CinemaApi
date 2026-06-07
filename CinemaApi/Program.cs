using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CinemaDbContext>(options =>
options.UseInMemoryDatabase("CinemaDb"));

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CinemaDbContext>();
    db.Database.EnsureCreated();

    if (!db.Movies.Any())
    {

        db.Movies.AddRange(
            new Movie { Title = "Inception", Genre = "Sci-Fi", DurationMinutes = 148 },
            new Movie { Title = "The Matrix", Genre = "Sci-Fi", DurationMinutes = 136 },
            new Movie { Title = "Interstellar", Genre = "Sci-Fi", DurationMinutes = 169 }
        );

        db.Halls.AddRange(
            new Hall { SeatsCount = 100 },
            new Hall { SeatsCount = 80 },
            new Hall { SeatsCount = 50 }
        );

        db.SaveChanges();

        db.Sessions.AddRange(
            new Session { MovieId = 1, HallId = 1, StartTime = DateTime.Today.AddHours(14) },
            new Session { MovieId = 1, HallId = 2, StartTime = DateTime.Today.AddHours(18) },
            new Session { MovieId = 2, HallId = 1, StartTime = DateTime.Today.AddHours(16) },
            new Session { MovieId = 3, HallId = 3, StartTime = DateTime.Today.AddHours(20) }
        );

        db.SaveChanges();

        db.Tickets.AddRange(
            new Ticket { SessionId = 1, CustomerName = "John Doe", SeatNumber = 5 },
            new Ticket { SessionId = 1, CustomerName = "Jane Smith", SeatNumber = 6 },
            new Ticket { SessionId = 2, CustomerName = "Alice Johnson", SeatNumber = 10 },
            new Ticket { SessionId = 3, CustomerName = "Bob Brown", SeatNumber = 15 }
        );

        db.SaveChanges();
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
