using ReportService.Domain.Models;

namespace ReportService.Application.Interfaces;

public interface IReportBuilder
{
    void BuildLogo();
    void BuildTitle(String title);
    void BuildData(List<(string Encargado, string Ci, string Establecimiento, string Licencia, string Direccion)> data);
    void BuildChart();
    void BuildFooter();
    Report GetReport();
}