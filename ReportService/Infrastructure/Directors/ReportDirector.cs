using ReportService.Application.Interfaces;
using ReportService.Domain.Models;

namespace ReportService.Infrastructure.Directors;

public class ReportDirector
{
    public Report BuildXlsxReport(
        IReportBuilder<(string Encargado, string Ci, string Establecimiento, string Licencia, string Direccion)> builder,
        string title,
        List<(string Encargado, string Ci, string Establecimiento, string Licencia, string Direccion)> data)
    {
        builder.BuildLogo();
        builder.BuildTitle(title);
        builder.BuildData(data);
        builder.BuildFooter();
        // builder.BuildChart();
        return builder.GetReport();
    }
}