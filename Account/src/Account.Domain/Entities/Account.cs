using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Account.Domain.Entities;

public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Unicamente números y letras están permitidos")]
    [MaxLength(25)]
    public string Number { get; set; }

    [Required]
    [Range(0.01, int.MaxValue, ErrorMessage = "El deposito inicial debe ser mayor a cero")]
    public decimal OpeningDeposit { get; set; }

    [Required]
    [Range(0.01, int.MaxValue, ErrorMessage = "El balance no puede ser negativo")]
    public decimal Balance { get; set; }

    public bool Status { get; set; }

    [Required]
    public int ClientId { get; set; }

    public Account()
    {
        Status = true;
    }
}
