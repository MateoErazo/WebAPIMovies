using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.DTOs
{
  public class GenderPutDTO
  {
    [Required]
    [StringLength(40)]
    public string Name { get; set; }
  }
}
