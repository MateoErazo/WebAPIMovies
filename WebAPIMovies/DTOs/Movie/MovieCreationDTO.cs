﻿using System.ComponentModel.DataAnnotations;
using WebAPIMovies.Validations;

namespace WebAPIMovies.DTOs.Movie
{
  public class MovieCreationDTO
  {

    [Required]
    [StringLength(150)]
    public string Title { get; set; }

    public bool IsInCinema { get; set; }

    public DateTime ReleaseDate { get; set; }

    [FileWeight(maximumWeightInMegaBytes:2)]
    [FileType(GroupFileType.Picture)]
    public IFormFile Poster { get; set; }

    public List<int> GendersIds { get; set; }
  }
}
