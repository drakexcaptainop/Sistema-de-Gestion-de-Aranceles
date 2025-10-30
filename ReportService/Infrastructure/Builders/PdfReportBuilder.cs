using QuestPDF.Companion;
using QuestPDF.Fluent;
using ReportService.Application.Interfaces;
using ReportService.Domain.Models;
using QuestPDF.Infrastructure;

namespace ReportService.Infrastructure.Builders;

public class PdfReportBuilder: IReportBuilder
{
    public Report Report = new();
    public Document doc = Document.Create(container => {});

    public void BuildLogo()
    {
        
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

    public object GenerarPdf()
    {
        throw new NotImplementedException();
    }

    public Report GetReport()
    {
        return new Report
        {
            Logo = Report.Logo,
            Title = Report.Title,
            Data = Report.Data,
            Footer = Report.Footer,
            Result = GenerarPdf()
        };
    }
}