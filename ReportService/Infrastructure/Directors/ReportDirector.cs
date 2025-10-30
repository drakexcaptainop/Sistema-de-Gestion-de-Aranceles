using ReportService.Application.Interfaces;
using ReportService.Domain.Models;

namespace ReportService.Infrastructure.Directors;

public class ReportDirector
{
    public Report BuildReport(IReportBuilder builder, string title, List<string> data)
    {
        builder.BuildLogo();
        builder.BuildTitle(title);
        builder.BuildData(data);
        builder.BuildFooter();
        // builder.BuildChart();
        return builder.GetReport();
    }
}