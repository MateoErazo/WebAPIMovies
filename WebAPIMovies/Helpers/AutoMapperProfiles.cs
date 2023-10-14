using AutoMapper;
using Microsoft.Identity.Client;
using WebAPIMovies.DTOs.Actor;
using WebAPIMovies.DTOs.Gender;
using WebAPIMovies.DTOs.Movie;
using WebAPIMovies.DTOs.MoviesActors;
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

      CreateMap<Actor, ActorPatchDTO>().ReverseMap();

      CreateMap<Movie, MovieDTO>();

      CreateMap<MovieCreationDTO, Movie>()
      .ForMember(x => x.MoviesGenders, options => options.MapFrom(MapMoviesGenders))
      .ForMember(x => x.MoviesActors, options => options.MapFrom(MapMoviesActors));

      CreateMap<MoviePutDTO, Movie>()
      .ForMember(x => x.Poster, options => options.Ignore())
      .ForMember(x => x.MoviesGenders, options => options.MapFrom(MapMoviesGendersPut))
      .ForMember(x => x.MoviesActors, options => options.MapFrom(MapMoviesActorsPut)); ;

      CreateMap<Movie, MoviePatchDTO>().ReverseMap();
    }

    private List<MoviesGenders> MapMoviesGendersPut(MoviePutDTO moviePutDTO, Movie movie)
    {
      List<MoviesGenders> result = new List<MoviesGenders>();

      if (moviePutDTO.GendersIds is null)
      {
        return result;
      }

      foreach (int id in moviePutDTO.GendersIds)
      {
        result.Add(new MoviesGenders() { GenderId = id });
      }

      return result;
    }

    private List<MoviesActors> MapMoviesActorsPut(MoviePutDTO moviePutDTO, Movie movie)
    {
      List<MoviesActors> result = new List<MoviesActors>();

      if (moviePutDTO.Actors is null)
      {
        return result;
      }

      foreach (ActorMoviesCreationDTO actor in moviePutDTO.Actors)
      {
        result.Add(new MoviesActors() { ActorId = actor.ActorId, Character = actor.Character });
      }

      return result;
    }

    private List<MoviesGenders> MapMoviesGenders(MovieCreationDTO movieCreationDTO, Movie movie)
    {
      List<MoviesGenders> result = new List<MoviesGenders>();

      if(movieCreationDTO.GendersIds is null)
      {
        return result;
      }

      foreach (int id in movieCreationDTO.GendersIds)
      {
        result.Add(new MoviesGenders() { GenderId = id});
      }

      return result;
    }

    private List<MoviesActors> MapMoviesActors(MovieCreationDTO movieCreationDTO, Movie movie)
    {
      List<MoviesActors> result = new List<MoviesActors>();

      if (movieCreationDTO.Actors is null)
      {
        return result;
      }

      foreach (ActorMoviesCreationDTO actor in movieCreationDTO.Actors)
      {
        result.Add(new MoviesActors() { ActorId = actor.ActorId, Character = actor.Character});
      }

      return result;
    }
  }
}
