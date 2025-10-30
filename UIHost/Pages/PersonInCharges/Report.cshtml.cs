using Common.Domain.SharedPorts;
using EstablishmentService.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UIHost.Security;

namespace UIHost.Pages.PersonInCharges
{
    public class ReportModel : PageModel
    {

        private readonly EstablishmentReportService _establishmentReportService;

        [BindProperty]
        public DateTime? FechaInicio { get; set; }

        [BindProperty]
        public string? EstablishmentType { get; set; }

        public ReportModel(EstablishmentReportService establishmentReportService)
        {
            _establishmentReportService = establishmentReportService;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostFiltrar()
        {
            var reportService = _establishmentReportService;
            if (reportService == null)
            {
                TempData["ErrorMessage"] = "No se pudo obtener el servicio de reporte.";
                return RedirectToPage();
            }

            string createdBy = User.Identity?.Name ?? "Usuario desconocido";

            var report = reportService.GenerateEstablishmentPersonInChargeReport(createdBy, FechaInicio, EstablishmentType);

            if (report == null || report.Result == null)
            {
                TempData["ErrorMessage"] = "No se pudo generar el reporte.";
                return RedirectToPage();
            }

            byte[] fileBytes = (byte[])report.Result;
            string fileName = $"Reporte_Encargados_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";

            return File(fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
    }
}
