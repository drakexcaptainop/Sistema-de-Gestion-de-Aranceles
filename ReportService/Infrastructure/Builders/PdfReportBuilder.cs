using ReportService.Application.Interfaces;
using ReportService.Domain.Models;

namespace ReportService.Infrastructure.Builders;

public class PdfReportBuilder: IReportBuilder
{
    public void BuildLogo()
    {
        throw new NotImplementedException();
    }
    
    public void BuildTitle(string title)
    {
        throw new NotImplementedException();
    }
    
    public void BuildData(List<(string Encargado, string Ci, string Establecimiento, string Licencia, string Direccion)> data)
    {
        throw new NotImplementedException();
    }

    public void BuildChart()
    {
        throw new NotImplementedException();
    }

    public void BuildFooter()
    {
        throw new NotImplementedException();
    }

    public Report GetReport()
    {
        throw new NotImplementedException();
    }
}