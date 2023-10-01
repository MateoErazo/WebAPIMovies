using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.DTOs.Gender
{
    public class GenderCreationDTO
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
