using ReportService.Domain.Models;

namespace ReportService.Application.Interfaces;

public interface IReportBuilder<T>
{
    void BuildLogo();
    void BuildTitle(String title);
    void BuildData(List<T> data);
    void BuildChart();
    void BuildFooter();
    Report GetReport();
}