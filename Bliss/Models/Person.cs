using System.ComponentModel.DataAnnotations;

namespace Task.Models;

public class Person
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    [Required]

    public DateTime DateOfBirth { get; set; }
    public bool Married { get; set; }
    [Required]
    public string Phone { get; set; }
    
    public double Salary { get; set; }
}