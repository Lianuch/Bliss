namespace Task.Models;

public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Married { get; set; }
    public string Phone { get; set; }
    public decimal Salary { get; set; }
}