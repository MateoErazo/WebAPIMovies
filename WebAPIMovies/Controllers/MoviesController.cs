using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIMovies.DTOs.Actor;
using WebAPIMovies.DTOs.Movie;
using WebAPIMovies.Entities;
using WebAPIMovies.Migrations;
using WebAPIMovies.Services;

namespace WebAPIMovies.Controllers
{

  [ApiController]
  [Route("api/movies")]
  public class MoviesController:ControllerBase
  {
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly IFileStorage fileStorage;
    private readonly string containerName = "movies";

    public MoviesController(
      ApplicationDbContext context,
      IMapper mapper,
      IFileStorage fileStorage) 
    {
      this.context = context;
      this.mapper = mapper;
      this.fileStorage = fileStorage;
    }

    /// <summary>
    /// Get a list of movies in DB
    /// </summary>
    /// <returns>List of movies in DB</returns>
    [HttpGet(Name ="getMovies")]
    public async Task<ActionResult<List<MovieDTO>>> GetMovies()
    {
      List<Movie> movies = await context.Movies.ToListAsync();
      return mapper.Map<List<MovieDTO>>(movies);
    }

    /// <summary>
    /// Get a specific movie from DB by It's Id
    /// </summary>
    /// <param name="id">Id movie</param>
    /// <returns>Movie object</returns>
    [HttpGet("{id:int}",Name ="getMovieById")]
    public async Task<ActionResult<MovieDTO>> GetMovieById(int id)
    {
      Movie movie = await context.Movies.FirstOrDefaultAsync(x => x.Id == id);

      if (movie is null)
      {
        return NotFound();
      }

      return mapper.Map<MovieDTO>(movie);
    }

    /// <summary>
    /// Create a new movie in the DB
    /// </summary>
    /// <param name="movieCreationDTO">Object movie with the data to create</param>
    /// <returns></returns>
    [HttpPost(Name ="addNewMovie")]
    public async Task<ActionResult> AddNewMovie([FromForm] MovieCreationDTO movieCreationDTO)
    {
      Movie movie = mapper.Map<Movie>(movieCreationDTO);

      if (movieCreationDTO.Poster != null)
      {
        using (var ms = new MemoryStream())
        {
          await movieCreationDTO.Poster.CopyToAsync(ms);
          var content = ms.ToArray();
          var extension = Path.GetExtension(movieCreationDTO.Poster.FileName);

          movie.Poster = await fileStorage.SaveFile(
            content: content,
            extension: extension,
          container: containerName,
            contentType: movieCreationDTO.Poster.ContentType);
        }
      }

      SetOrderActors(movie);
      context.Add(movie);
      await context.SaveChangesAsync();
      MovieDTO movieDTO = mapper.Map<MovieDTO>(movie);
      return CreatedAtRoute("getMovieById", new { id= movie.Id}, movieDTO);
      
    }

    private void SetOrderActors(Movie movie)
    {
      if (movie.MoviesActors != null)
      {
        for (int i=0; i<movie.MoviesActors.Count; i++)
        {
          movie.MoviesActors[i].Order = i;
        }
      }
    }


    /// <summary>
    /// Update an specific movie by It's Id
    /// </summary>
    /// <param name="id">Id movie</param>
    /// <param name="movieUpdateDTO">Object movie with the new data</param>
    /// <returns></returns>
    [HttpPut("{id:int}",Name ="putMovieById")]
    public async Task<ActionResult> PutMovieById(int id, [FromForm] MoviePutDTO movieUpdateDTO)
    {
      Movie movieDB = await context.Movies
        .Include(x=>x.MoviesActors)
        .Include(x=>x.MoviesGenders)
        .FirstOrDefaultAsync(x => x.Id == id);

      if (movieDB is null)
      {
        return NotFound();
      }

      movieDB = mapper.Map(movieUpdateDTO, movieDB);

      if (movieUpdateDTO.Poster != null)
      {
        using (var ms = new MemoryStream())
        {
          await movieUpdateDTO.Poster.CopyToAsync(ms);
          var content = ms.ToArray();
          var extension = Path.GetExtension(movieUpdateDTO.Poster.FileName);

          movieDB.Poster = await fileStorage.EditFile(
            content: content,
            extension: extension,
            container: containerName,
            path: movieDB.Poster,
            contentType: movieUpdateDTO.Poster.ContentType);
        }
      }

      SetOrderActors(movieDB);
      await context.SaveChangesAsync();
      return NoContent();
    }


    /// <summary>
    /// Update the specific movie property that receive in the body of request
    /// </summary>
    /// <param name="id">Id movie</param>
    /// <param name="jsonPatchDocument">json patch object with the new data</param>
    /// <returns></returns>
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> PatchMovie(int id, [FromBody] JsonPatchDocument<MoviePatchDTO> jsonPatchDocument)
    {
      if (jsonPatchDocument is null)
      {
        return BadRequest();
      }

      Movie movieDB = await context.Movies.FirstOrDefaultAsync(x => x.Id == id);

      if (movieDB is null)
      {
        return NotFound();
      }

      MoviePatchDTO moviePatchDTO = mapper.Map<MoviePatchDTO>(movieDB);
      jsonPatchDocument.ApplyTo(moviePatchDTO, ModelState);

      bool isValidModel = TryValidateModel(moviePatchDTO);

      if (!isValidModel)
      {
        return BadRequest(ModelState);
      }

      mapper.Map(moviePatchDTO, movieDB);

      await context.SaveChangesAsync();
      return NoContent();

    }

    /// <summary>
    /// Delete an specific movie permanently
    /// </summary>
    /// <param name="id">Id movie to delete</param>
    /// <returns></returns>
    [HttpDelete("{id:int}", Name = "deleteMovie")]
    public async Task<ActionResult> DeleteMovie(int id)
    {
      bool existMovie = await context.Movies.AnyAsync(g => g.Id == id);

      if (!existMovie)
      {
        return NotFound();
      }

      context.Remove(new Movie { Id = id });
      await context.SaveChangesAsync();
      return NoContent();
    }

  }
}
