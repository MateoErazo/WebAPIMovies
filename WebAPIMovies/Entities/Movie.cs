using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.Entities
{
  public class Movie
  {
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Title { get; set; }

    public bool IsInCinema { get; set; }

    public DateTime ReleaseDate { get; set; }

    public string Poster { get; set; }
  }
}
