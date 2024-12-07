using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Domain.Entities;

public class Client : Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 5)]
    public string Password { get; set; }

    [Required]
    public bool Status { get; set; }

    public Client()
    {
        Status = true;
    }
}
