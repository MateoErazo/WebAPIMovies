using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Controllers
{
  [ApiController]
  [Route("api/genders")]
  public class GendersController:ControllerBase
  {
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public GendersController(
      ApplicationDbContext context,
      IMapper mapper
      ) 
    {
      this.context = context;
      this.mapper = mapper;
    }

    /// <summary>
    /// Get a list of genders of the DB
    /// </summary>
    /// <returns>list of genders</returns>
    [HttpGet(Name ="getGenders")]
    public async Task<ActionResult<List<GenderDTO>>> GetGenders()
    {
      List<Gender> genders = await context.Genders.ToListAsync();
      List<GenderDTO> gendersDTO = mapper.Map<List<GenderDTO>>( genders );
      return gendersDTO;
    }

    /// <summary>
    /// Get an specific gender by It's Id
    /// </summary>
    /// <param name="id">Id gender</param>
    /// <returns>Specific gender</returns>
    [HttpGet("{id:int}",Name ="getGenderById")]
    public async Task<ActionResult<GenderDTO>> GetGenderById(int id)
    {
      var gender = await context.Genders.FirstOrDefaultAsync( g => g.Id == id );

      if (gender is null)
      {
        return NotFound();
      }

      return mapper.Map<GenderDTO>(gender);

    }

    /// <summary>
    /// Add a new gender in the DB
    /// </summary>
    /// <param name="genderCreationDTO">object gender</param>
    /// <returns>object created if It's successful process</returns>
    [HttpPost(Name ="postGender")]
    public async Task<ActionResult> PostGender([FromBody] GenderCreationDTO genderCreationDTO)
    {
      Gender gender = mapper.Map<Gender>(genderCreationDTO);
      context.Add(gender);
      await context.SaveChangesAsync();
      GenderDTO genderDTO = mapper.Map<GenderDTO>(gender);
      return CreatedAtRoute("getGenderById", new {id = gender.Id}, genderDTO);
    }


    /// <summary>
    /// Make a complete Update to a gender by It's Id
    /// </summary>
    /// <param name="id">Id gender</param>
    /// <param name="genderPutDTO">gender object with the new data</param>
    /// <returns></returns>
    [HttpPut("{id:int}",Name ="putGender")]
    public async Task<ActionResult> PutGender(int id, [FromBody] GenderPutDTO genderPutDTO)
    {
      bool existGender = await context.Genders.AnyAsync(g => g.Id == id);

      if (!existGender)
      {
        return NotFound();
      }

      Gender gender = mapper.Map<Gender>(genderPutDTO);
      gender.Id = id;
      context.Entry(gender).State = EntityState.Modified;
      await context.SaveChangesAsync();
      return NoContent();

    }


    [HttpDelete("{id:int}",Name ="deleteGender")]
    public async Task<ActionResult> DeleteGender(int id)
    {
      bool existGender = await context.Genders.AnyAsync(g => g.Id == id);

      if (!existGender)
      {
        return NotFound();
      }

      context.Remove(new Gender { Id = id});
      await context.SaveChangesAsync();
      return NoContent();
    }


  }
}
