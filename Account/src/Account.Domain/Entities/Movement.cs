using Account.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Account.Domain.Entities;

public class Movement
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime Timestamp { get; private set; }

    [Required]
    public AccountTypes AccountType { get; set; }

    public decimal Amount { get; set; }

    [Required]
    [Range(0.01, int.MaxValue, ErrorMessage = "El balance no puede ser negativo")]
    public decimal Balance { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 5)]
    public string AccountNumber { get; set; }

    public Movement()
    {
        Timestamp = DateTime.UtcNow;
    }
}
