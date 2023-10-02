using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAPIMovies.DTOs.Actor;
using WebAPIMovies.Entities;
using WebAPIMovies.Services;

namespace WebAPIMovies.Controllers
{
  [ApiController]
  [Route("api/actors")]
  public class ActorsController:ControllerBase
  {
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly IFileStorage fileStorage;
    private readonly string containerName = "actors";

    public ActorsController(
      ApplicationDbContext context,
      IMapper mapper,
      IFileStorage fileStorage) 
    {
      this.context = context;
      this.mapper = mapper;
      this.fileStorage = fileStorage;
    }

    /// <summary>
    /// Get a list of actors of the DB
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name ="getActors")]
    public async Task<ActionResult<List<ActorDTO>>> GetActors()
    {
      List<Actor> actors = await context.Actors.ToListAsync();
      List<ActorDTO> actorDTOs = mapper.Map<List<ActorDTO>>(actors);
      return actorDTOs;
    }

    /// <summary>
    /// Get an specifyc actor by It's Id
    /// </summary>
    /// <param name="id">Id actor</param>
    /// <returns>Actor object if exist</returns>
    [HttpGet("{id:int}",Name ="getActorById")]
    public async Task<ActionResult<ActorDTO>> GetActorById(int id)
    {
      Actor actor = await context.Actors.FirstOrDefaultAsync(x => x.Id == id);

      if(actor is null)
      {
        return NotFound();
      }

      return mapper.Map<ActorDTO>(actor);
    }

    /// <summary>
    /// Create a new actor in DB
    /// </summary>
    /// <param name="actorCreationDTO"></param>
    /// <returns>Actor created if It's successful process</returns>
    [HttpPost(Name ="addNewActor")]
    public async Task<ActionResult> AddNewActor([FromForm] ActorCreationDTO actorCreationDTO)
    {
      Actor actor = mapper.Map<Actor>(actorCreationDTO);

      if (actorCreationDTO.Picture != null)
      {
        using (var ms = new MemoryStream())
        {
          await actorCreationDTO.Picture.CopyToAsync(ms);
          var content = ms.ToArray();
          var extension = Path.GetExtension(actorCreationDTO.Picture.FileName);

          actor.Picture = await fileStorage.SaveFile(
            content:content,
            extension:extension,
            container:containerName,
            contentType:actorCreationDTO.Picture.ContentType);
        }
      }

      context.Add(actor);
      await context.SaveChangesAsync();

      ActorDTO actorDTO = mapper.Map<ActorDTO>(actor);

      return CreatedAtRoute("getActorById", new {id=actor.Id}, actorDTO);
    }

    /// <summary>
    /// Update an specific actor by It's Id
    /// </summary>
    /// <param name="id">Id Actor</param>
    /// <param name="actorPutDTO">Actor Object with the new data</param>
    /// <returns></returns>
    [HttpPut("{id:int}",Name ="putActor")]
    public async Task<ActionResult> PutActor(int id, [FromForm] ActorPutDTO actorPutDTO)
    {
      Actor actorDB = await context.Actors.FirstOrDefaultAsync(x => x.Id == id);
      
      if(actorDB is null)
      {
        return NotFound();
      }

      actorDB = mapper.Map(actorPutDTO,actorDB);

      if (actorPutDTO.Picture != null)
      {
        using (var ms = new MemoryStream())
        {
          await actorPutDTO.Picture.CopyToAsync(ms);
          var content = ms.ToArray();
          var extension = Path.GetExtension(actorPutDTO.Picture.FileName);

          actorDB.Picture = await fileStorage.EditFile(
            content: content,
            extension: extension,
            container: containerName,
            path:actorDB.Picture,
            contentType: actorPutDTO.Picture.ContentType);
        }
      }

      await context.SaveChangesAsync();
      return NoContent();

    }

    /// <summary>
    /// Delete an specific actor permanently
    /// </summary>
    /// <param name="id">Id actor to delete</param>
    /// <returns></returns>
    [HttpDelete("{id:int}", Name = "deleteActor")]
    public async Task<ActionResult> DeleteActor(int id)
    {
      bool existActor = await context.Actors.AnyAsync(g => g.Id == id);

      if (!existActor)
      {
        return NotFound();
      }

      context.Remove(new Actor { Id = id });
      await context.SaveChangesAsync();
      return NoContent();
    }

  }
}
