using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReportService.Application.Services;
using ReportService.Domain.Models;

namespace UIHost.Pages.Fees
{
    public class ReportModel : PageModel
    {
        private readonly FeeReportService _reportService;
        [BindProperty(SupportsGet = true)] public string? ReportType { get; set; }
        [BindProperty(SupportsGet = true)] public DateTime StartDate { get; set; } = DateTime.Now;
        [BindProperty(SupportsGet = true)] public DateTime EndDate { get; set; } = DateTime.Now;
        [BindProperty(SupportsGet = true)] public decimal MinAmount { get; set; } = 50;
        [BindProperty(SupportsGet = true)] public decimal MaxAmount { get; set; } = 850;

        
        public void OnGet()
        {
            Console.WriteLine("OnGet triggered");
        }
        public ReportModel(FeeReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult OnPostGenerateReport()
        {
            // Apply filters (StartDate, EndDate, MinAmount, MaxAmount) as needed
            // You can inject your service/builder here via DI

            if (string.Equals(ReportType, "pdf", StringComparison.OrdinalIgnoreCase))
            {
                // Generate PDF (e.g., using QuestPDF)
                byte[] pdfBytes = GeneratePdfReport();
                return File(pdfBytes, "application/pdf", "reporte_aranceles.pdf");
            }

            if (string.Equals(ReportType, "excel", StringComparison.OrdinalIgnoreCase))
            {
                // Generate Excel (e.g., using ClosedXML, EPPlus, etc.)
                byte[] xlsxBytes = GenerateExcelReport();
                return File(xlsxBytes, 
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                    "reporte_aranceles.xlsx");
            }
            

            // If invalid type, reload the page
            return Page();
        }

        private byte[] GeneratePdfReport()
        {
            Report report = _reportService.GeneratePdf(StartDate, EndDate, MinAmount, MaxAmount);
            return report.Result;
        }

        private byte[] GenerateExcelReport()
        {
            // Placeholder: integrate with your ExcelReportBuilder here
            using var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("Demo Excel content");
            writer.Flush();
            return stream.ToArray();
        }
    }
}
