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
                new Movie { Title = "Interstellar", Genre = "Sci-Fi", DurationMinutes = 169 },
                new Movie { Title = "The Shawshank Redemption", Genre = "Drama", DurationMinutes = 142 },
                new Movie { Title = "The Godfather", Genre = "Crime", DurationMinutes = 175 },
                new Movie { Title = "Pulp Fiction", Genre = "Crime", DurationMinutes = 154 },
                new Movie { Title = "The Dark Knight", Genre = "Action", DurationMinutes = 152 },
                new Movie { Title = "Forrest Gump", Genre = "Drama", DurationMinutes = 142 },
                new Movie { Title = "Fight Club", Genre = "Drama", DurationMinutes = 139 },
                new Movie { Title = "Goodfellas", Genre = "Crime", DurationMinutes = 146 },
                new Movie { Title = "The Lord of the Rings: The Fellowship of the Ring", Genre = "Fantasy", DurationMinutes = 178 },
                new Movie { Title = "Star Wars: Episode V - The Empire Strikes Back", Genre = "Sci-Fi", DurationMinutes = 124 },
                new Movie { Title = "Parasite", Genre = "Thriller", DurationMinutes = 132 },
                new Movie { Title = "Spirited Away", Genre = "Animation", DurationMinutes = 125 },
                new Movie { Title = "The Prestige", Genre = "Mystery", DurationMinutes = 130 },
                new Movie { Title = "Avengers: Endgame", Genre = "Action", DurationMinutes = 181 },
                new Movie { Title = "Joker", Genre = "Drama", DurationMinutes = 122 }
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
