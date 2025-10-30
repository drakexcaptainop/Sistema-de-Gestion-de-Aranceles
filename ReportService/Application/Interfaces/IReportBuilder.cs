using ReportService.Domain.Models;

namespace ReportService.Application.Interfaces;

public interface IReportBuilder
{
    void BuildLogo();
    void BuildTitle(String title);
    void BuildData(List<String> data);
    void BuildChart();
    void BuildFooter();
    Report GetReport();
}