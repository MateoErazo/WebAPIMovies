using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebAPIMovies.DTOs.MoviesActors;
using WebAPIMovies.Helpers;
using WebAPIMovies.Validations;

namespace WebAPIMovies.DTOs.Movie
{
  public class MoviePutDTO
  {

    [Required]
    [StringLength(150)]
    public string Title { get; set; }

    public bool IsInCinema { get; set; }

    public DateTime ReleaseDate { get; set; }

    [FileWeight(maximumWeightInMegaBytes: 2)]
    [FileType(GroupFileType.Picture)]
    public IFormFile Poster { get; set; }

    [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
    public List<int> GendersIds { get; set; }

    [ModelBinder(BinderType = typeof(TypeBinder<List<ActorMoviesCreationDTO>>))]
    public List<ActorMoviesCreationDTO> Actors { get; set; }
  }
}
