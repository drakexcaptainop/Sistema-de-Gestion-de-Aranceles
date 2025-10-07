namespace SistemaDePagoDeAranceles.Models;

public class PersonInCharge
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Ci { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool Status  { get; set; }
    public int CreatedBy {get;set;}
    
}