﻿using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.DTOs.Actor
{
  public class ActorDTO
  {
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Picture { get; set; }
  }
}
