using Client.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Client.Domain.Entities;

public class Person
{
    [StringLength(3, MinimumLength = 100)]
    public required string Name { get; set; }

    public Genders? Gender { get; set; }

    public int? Age { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 3)]
    public required string DocumentNumber { get; set; }

    [StringLength(500, MinimumLength = 3)]
    public string? Address { get; set; }

    [StringLength(20, MinimumLength = 7)]
    public string? Phone { get; set; }
}
