using Microsoft.EntityFrameworkCore;
using WebAPIMovies.Entities;

namespace WebAPIMovies
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<MoviesGenders>().HasKey(x => new {x.MovieId, x.GenderId});
      modelBuilder.Entity<MoviesActors>().HasKey(x => new {x.MovieId, x.ActorId});

      base.OnModelCreating(modelBuilder);
    }

    public DbSet<Gender> Genders { get; set; }

    public DbSet<Actor> Actors { get; set; }

    public DbSet<Movie> Movies { get; set; }

    public DbSet<MoviesGenders> MoviesGenders { get; set; }

    public DbSet<MoviesActors> MoviesActors { get; set; }
  }
}
