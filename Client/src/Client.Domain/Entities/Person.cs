using Client.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Client.Domain.Entities;

public class Person
{
    public Person(string name, string documentNumber)
    {
        Name = name;
        DocumentNumber = documentNumber;
    }

    [StringLength(100, MinimumLength = 3)]
    public string? Name { get; set; } = string.Empty;

    public Genders? Gender { get; set; }

    public int? Age { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 3)]
    public string? DocumentNumber { get; set; } = string.Empty;

    [StringLength(500, MinimumLength = 3)]
    public string? Address { get; set; }

    [StringLength(20, MinimumLength = 7)]
    public string? Phone { get; set; }
}
