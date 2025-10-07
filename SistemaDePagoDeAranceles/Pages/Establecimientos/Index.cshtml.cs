using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDePagoDeAranceles.Models;
using System.Collections.Generic;

namespace SistemaDePagoDeAranceles.Pages.Establecimientos
{
    public class IndexModel : PageModel
    {
        public List<Test> Establecimientos { get; set; } = new();

        public void OnGet()
        {
            // Simulación temporal de datos (hasta que conectes tu BD)
            Establecimientos = new List<Test>
            {
                new Test {  Name = "Colegio San Martín", Description = "Av. Central 123" },
                new Test {  Name = "Escuela Bolivia", Description = "Calle Sucre 45" }
            };
        }
    }
}