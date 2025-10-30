using ReportService.Application.Interfaces;
using ReportService.Domain.Models;
using ClosedXML.Excel;

public class XlsxReportBuilder : IReportBuilder
{
    private XLWorkbook _workbook;
    private IXLWorksheet _sheet;
    private Report _report;

    public XlsxReportBuilder()
    {
        _workbook = new XLWorkbook();
        _sheet = _workbook.Worksheets.Add("Reporte");
        _report = new Report();
    }

    public void BuildLogo()
    {
    }

    public void BuildTitle(string title)
    {
        _sheet.Cell("A1").Value = title;
        _sheet.Cell("A1").Style.Font.Bold = true;
        _sheet.Cell("A1").Style.Font.FontSize = 14;
    }

    public void BuildData(List<(string Encargado, string Ci, string Establecimiento, string Licencia, string Direccion)> data)
    {
        int row = 2;
        _sheet.Cell(row, 1).Value = "Encargado";
        _sheet.Cell(row, 2).Value = "CI";
        _sheet.Cell(row, 3).Value = "Establecimiento";
        _sheet.Cell(row, 4).Value = "Licencia Sanitaria";
        _sheet.Cell(row, 5).Value = "Dirección";
        _sheet.Range(row, 1, row, 5).Style.Font.Bold = true;

        row++;
        foreach (var item in data)
        {
            _sheet.Cell(row, 1).Value = item.Encargado;
            _sheet.Cell(row, 2).Value = item.Ci;
            _sheet.Cell(row, 3).Value = item.Establecimiento;
            _sheet.Cell(row, 4).Value = item.Licencia;
            _sheet.Cell(row, 5).Value = item.Direccion;
            row++;
        }

        // Footer
        if (!string.IsNullOrEmpty(_report.Footer))
        {
            row += 2;
            _sheet.Cell(row, 1).Value = _report.Footer;
            _sheet.Cell(row, 1).Style.Font.Italic = true;
        }

        _sheet.Columns().AdjustToContents();
    }

    public void BuildChart()
    {
    }

    public void BuildFooter()
    {
    }

    public Report GetReport()
    {
        using var ms = new MemoryStream();
        _workbook.SaveAs(ms);
        _report.Result = ms.ToArray();
        return _report;
    }
    
    public void SetFooter(string footer)
    {
        _report.Footer = footer;
    }

}