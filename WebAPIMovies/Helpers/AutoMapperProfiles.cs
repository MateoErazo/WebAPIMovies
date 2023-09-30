using AutoMapper;
using Microsoft.Identity.Client;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Helpers
{
  public class AutoMapperProfiles:Profile
  {
    public AutoMapperProfiles() 
    {
      CreateMap<Gender, GenderDTO>().ReverseMap();

      CreateMap<GenderCreationDTO, Gender>();

      CreateMap<GenderPutDTO, Gender>();
    } 
  }
}
