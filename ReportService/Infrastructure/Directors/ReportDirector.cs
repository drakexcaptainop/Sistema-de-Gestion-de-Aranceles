using ReportService.Application.Interfaces;
using ReportService.Domain.Models;
using TariffingService.Domain.Models;

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

    public Report BuildPdfReport(IReportBuilder<Fee> builder, string title, List<Fee> data)
    {
        builder.BuildLogo();
        builder.BuildTitle(title);
        builder.BuildData(data);
        builder.BuildChart();
        builder.BuildFooter();
        return builder.GetReport();
    }
}