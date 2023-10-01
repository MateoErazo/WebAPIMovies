using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.DTOs.Actor
{
  public class ActorPutDTO
  {
    [Required]
    [StringLength(120)]
    public string Name { get; set; }

    public DateTime DateOfBirth { get; set; }
  }
}
