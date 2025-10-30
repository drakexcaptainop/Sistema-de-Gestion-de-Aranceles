using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlX.XDevAPI.Common;
using ReportService.Infrastructure.Builders;
using ReportService.Infrastructure.Directors;

namespace UIHost.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        ReportDirector director = new ReportDirector();
        PdfReportBuilder builder = new PdfReportBuilder();

        director.BuildReport(builder, "titulo ejemplo", ["dato1", "dato2", "dato3"]);


    }

}
