using CinemaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Data;

public class CinemaDbContext : DbContext
{
    public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) { }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Hall> Halls => Set<Hall>();
}