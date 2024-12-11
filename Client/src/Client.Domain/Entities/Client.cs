using Client.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Domain.Entities;

public class Client : Person
{
    public Client(string name,
        Genders? gender,
        int? age,
        string documentNumber,
        string address,
        string phone,
        string password,
        bool status) : base(name, documentNumber)
    {
        Gender = gender;
        Age = age;
        Address = address;
        Phone = phone;
        Password = password;
        Status = status;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 5)]
    public string Password { get; set; }

    [Required]
    public bool Status { get; set; }

    public void Update(string name,
        Genders? gender,
        int? age,
        string documentNumber,
        string address,
        string phone,
        string password,
        bool status)
    {
        Name = name;
        DocumentNumber = documentNumber;
        Gender = gender;
        Age = age;
        Address = address;
        Phone = phone;
        Password = password;
        Status = status;
    }

}
