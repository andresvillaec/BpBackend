using Client.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Client.Application.Dtos;

public class ClientDto
{
    [Required]
    public int Id { get; set; }

    [StringLength(3, MinimumLength = 100)]
    public required string Name { get; set; }

    [Range((int)Genders.Male, (int)Genders.Female, ErrorMessage = "Solo se permite los valores 1(Masculino) y 2(Femelino)")]
    public Genders? Gender { get; set; }

    [Range(0, 160, ErrorMessage = "Se permiten valores entre 0-160")]
    public int? Age { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Unicamente números y letras están permitidos")]
    [StringLength(25, MinimumLength = 3)]
    public required string DocumentNumber { get; set; }

    [StringLength(500, MinimumLength = 3)]
    public string? Address { get; set; }

    [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números están permitidos")]
    public string? Phone { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 5)]
    public string Password { get; set; }

    [Required]
    public bool Status { get; set; }

}
