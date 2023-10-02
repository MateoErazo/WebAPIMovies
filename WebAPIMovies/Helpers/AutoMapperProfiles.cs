using AutoMapper;
using Microsoft.Identity.Client;
using WebAPIMovies.DTOs.Actor;
using WebAPIMovies.DTOs.Gender;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Helpers
{
    public class AutoMapperProfiles:Profile
  {
    public AutoMapperProfiles() 
    {
      CreateMap<Gender, GenderDTO>();

      CreateMap<GenderCreationDTO, Gender>();

      CreateMap<GenderPutDTO, Gender>();

      CreateMap<Actor, ActorDTO>();

      CreateMap<ActorCreationDTO, Actor>();

      CreateMap<ActorPutDTO,Actor>()
        .ForMember(x=>x.Picture, options => options.Ignore());

    }
  }
}
