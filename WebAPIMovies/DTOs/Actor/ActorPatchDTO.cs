using System.ComponentModel.DataAnnotations;
using WebAPIMovies.Validations;

namespace WebAPIMovies.DTOs.Actor
{
  public class ActorPatchDTO
  {

    [Required]
    [StringLength(120)]
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }

  }
}
