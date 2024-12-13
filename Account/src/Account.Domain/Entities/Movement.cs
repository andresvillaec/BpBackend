using Microsoft.EntityFrameworkCore;
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

    [Precision(18, 2)]
    public decimal Amount { get; set; }

    [Required]
    [Range(0.01, int.MaxValue, ErrorMessage = "El balance no puede ser negativo")]
    [Precision(18, 2)]
    public decimal Balance { get; set; }

    public int AccountId { get; set; }

    public Account Account { get; set; }

    public Movement(
        decimal amount,
        decimal balance,
        int accountId
    )
    {
        Timestamp = DateTime.Now;
        Amount = amount;
        Balance = balance;
        AccountId = accountId;
    }

    public void Update(
        decimal amount,
        decimal balance
    )
    {
        Timestamp = DateTime.Now;
        Amount = amount;
        Balance = balance;
    }
}
