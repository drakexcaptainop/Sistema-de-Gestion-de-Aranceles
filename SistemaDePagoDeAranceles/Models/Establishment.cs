using System.ComponentModel.DataAnnotations;

namespace SistemaDePagoDeAranceles.Models
{
    public class Establishment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 3,ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string Name { get; set; }

        [StringLength(150, MinimumLength = 3,ErrorMessage = "El nombre comercial no debe exceder los 150 caracteres")]
        public string BusinessName { get; set; }

        [StringLength(20)]
        public string TaxId { get; set; }

        public string SanitaryLicense { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SanitaryLicenseExpiry { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(1000 ,MinimumLength = 3, ErrorMessage = "No sobre excede de los 1000 caracteres")]
        public string Address { get; set; }

        [Phone(ErrorMessage = "Número de teléfono inválido")]
        [StringLength(8, MinimumLength = 7, ErrorMessage = "Numero de telefono invalido")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El tipo de establecimiento es obligatorio")]
        public string EstablishmentType { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un encargado")]
        public int PersonInChargeId { get; set; }

        // Audit
        public DateTime RegisterDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Active { get; set; }
        public int CreatedBy { get; set; }
    }
}