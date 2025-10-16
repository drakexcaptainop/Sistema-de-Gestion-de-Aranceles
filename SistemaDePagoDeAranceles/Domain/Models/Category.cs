using System.ComponentModel.DataAnnotations;

namespace SistemaDePagoDeAranceles.Domain.Models;

public class Category
{
    public int Id { get; set; }

    [StringLength(50, MinimumLength = 3,ErrorMessage = "Nombre Invalido")]
    public string Name { get; set; }
    
    [StringLength(50, MinimumLength = 3 ,ErrorMessage = "Descripcion invalida")]
    public string Description { get; set; }

    [Required(ErrorMessage = "El monto base es obligatorio")]
    [Range(0, 100000, ErrorMessage = "El monto base debe ser positivo y menor a 100000")]
    public decimal BaseAmount { get; set; }

    public DateTime RegisterDate { get; set; }
    public DateTime LastUpdate { get; set; }
    public bool Status { get; set; }
    public int CreatedBy { get; set; }
}