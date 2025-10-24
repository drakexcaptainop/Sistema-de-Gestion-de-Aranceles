using System.ComponentModel.DataAnnotations;

namespace SistemaDePagoDeAranceles.Domain.Models;

public class PersonInCharge
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre es invalido")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El apellido es invalido")]
    public string LastName { get; set; }

    [EmailAddress(ErrorMessage = "Correo inválido")]
    public string Email { get; set; }

    [Phone(ErrorMessage = "Número de teléfono inválido")]
    [StringLength(8, MinimumLength = 7, ErrorMessage = "Numero de telefono invalido")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "El CI es obligatorio")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "El CI es invalido")]
    public string Ci { get; set; }
    public string Address { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool Status { get; set; }
    public int CreatedBy { get; set; }
}