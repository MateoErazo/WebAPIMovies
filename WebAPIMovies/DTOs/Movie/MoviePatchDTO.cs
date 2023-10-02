using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.DTOs.Movie
{
  public class MoviePatchDTO
  {

    [Required]
    [StringLength(150)]
    public string Title { get; set; }

    public bool IsInCinema { get; set; }

    public DateTime ReleaseDate { get; set; }
  }
}
