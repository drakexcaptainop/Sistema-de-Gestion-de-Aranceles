namespace SistemaDePagoDeAranceles.Domain.Models;

public class User
{
    public int Id { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public String Address { get; set; }
    public DateTime BirthDate { get; set; }
    public String Email { get; set; }
    public String Role { get; set; }
    public String HashedPassword { get; set; }
    public bool FirstLogin { get; set; }
}