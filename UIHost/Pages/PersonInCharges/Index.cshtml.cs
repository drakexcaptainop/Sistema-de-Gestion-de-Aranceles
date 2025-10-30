using Microsoft.AspNetCore.Mvc.RazorPages;

using EstablishmentService.Domain.Models;
using Common.Domain.Patterns;
using Common.Domain.SharedPorts;
using Microsoft.AspNetCore.Mvc;
using UIHost.Security;



namespace UIHost.Pages.PersonInCharges
{
    public class IndexModel : PageModel
    {
        private readonly IRepositoryService<PersonInCharge> _repository;
        private readonly IdProtector _idProtector;
        private readonly EstablishmentReportService _establishmentReportService;
        
        [BindProperty]
        public string SearchTerm { get; set; }
        public List<PersonInCharge> Persons { get; set; } = new();
        public Result<IEnumerable<PersonInCharge>> ResultGetAllPersonInCharge { get; set; }

        public IndexModel(IRepositoryServiceFactory<PersonInCharge> factory, IdProtector idProtector, EstablishmentReportService  establishmentReportService)
        {
            _establishmentReportService = establishmentReportService;
            _repository = factory.CreateRepositoryService();
            _idProtector = idProtector;
        }

        public void OnGet()
        {
            ResultGetAllPersonInCharge = _repository.GetAll();
        }

        public void OnPost()
        {
            ResultGetAllPersonInCharge = string.IsNullOrWhiteSpace(SearchTerm) ? _repository.GetAll() : _repository.Search(SearchTerm);
        }
        public IActionResult OnGetGenerateReport()
        {
            var reportService = _establishmentReportService;
            if (reportService == null)
            {
                TempData["ErrorMessage"] = "No se pudo obtener el servicio de reporte.";
                return RedirectToPage();
            }

            string createdBy = User.Identity?.Name ?? "Usuario desconocido";

            var report = reportService.GenerateEstablishmentPersonInChargeReport(createdBy, null, null);

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

        public string Protect(int id) => _idProtector.ProtectInt(id);
    }
}