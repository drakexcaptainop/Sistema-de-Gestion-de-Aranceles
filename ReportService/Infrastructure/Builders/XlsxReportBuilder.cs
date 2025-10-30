using ReportService.Application.Interfaces;
using ReportService.Domain.Models;

namespace ReportService.Infrastructure.Builders;

public class XlsxReportBuilder: IReportBuilder
{
    public void BuildLogo()
    {
        throw new NotImplementedException();
    }
    
    public void BuildTitle(string title)
    {
        throw new NotImplementedException();
    }
    
    public void BuildData(List<string> data)
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