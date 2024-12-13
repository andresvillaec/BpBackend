using Account.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Account.Domain.Entities;

public class Account
{
    public Account(
        string number,
        AccountTypes accountTypes,
        decimal openingDeposit,
        decimal balance,
        int clientId) : this()
    {
        Number = number;
        AccountType = accountTypes;
        OpeningDeposit = openingDeposit;
        Balance = balance;
        Status = true;
        ClientId = clientId;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public AccountTypes AccountType { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Unicamente números y letras están permitidos")]
    [MaxLength(25)]
    public string Number { get; set; }

    [Required]
    [Range(0.01, int.MaxValue, ErrorMessage = "El deposito inicial debe ser mayor a cero")]
    [Precision(18, 2)]
    public decimal OpeningDeposit { get; set; }

    [Required]
    [Range(0.01, int.MaxValue, ErrorMessage = "El balance no puede ser negativo")]
    [Precision(18, 2)]
    public decimal Balance { get; set; }

    public bool Status { get; set; }

    [Required]
    public int ClientId { get; set; }

    public ICollection<Movement> Movements { get; set; }

    public Account()
    {
        Status = true;
    }

    public void Update(
        string number,
        AccountTypes accountTypes,
        decimal openingDeposit,
        decimal balance,
        bool status,
        int clientId)
    {
        Number = number;
        AccountType = accountTypes;
        OpeningDeposit = openingDeposit;
        Balance = balance;
        Status = status;
        ClientId = clientId;
    }

    public void UpdateBalance(decimal newMovementAmount)
    {
        Balance += newMovementAmount;
    }
}
