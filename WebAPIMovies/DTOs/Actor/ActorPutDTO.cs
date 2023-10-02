using System.ComponentModel.DataAnnotations;
using WebAPIMovies.Validations;

namespace WebAPIMovies.DTOs.Actor
{
  public class ActorPutDTO
  {
    [Required]
    [StringLength(120)]
    public string Name { get; set; }

    public DateTime DateOfBirth { get; set; }

    [FileWeight(maximumWeightInMegaBytes: 2)]
    [FileType(groupFileType: GroupFileType.Picture)]
    public IFormFile Picture { get; set; }
  }
}
