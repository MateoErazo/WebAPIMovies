using Microsoft.EntityFrameworkCore;
using WebAPIMovies.Entities;

namespace WebAPIMovies
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Gender> Genders { get; set; }

    public DbSet<Actor> Actors { get; set; }
  }
}
