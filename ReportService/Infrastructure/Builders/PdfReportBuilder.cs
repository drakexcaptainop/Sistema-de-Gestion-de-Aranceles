using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ReportService.Application.Interfaces;
using ReportService.Domain.Models;
using TariffingService.Domain.Models;

namespace ReportService.Infrastructure.Builders;

public class PdfReportBuilder : IReportBuilder<Fee>
{
    private readonly Report _report = new();

    public void BuildLogo()
    {
        // TODO: Replace with your own local image path
        string logoPath = "/home/archflony/RiderProjects/arqui/Sistema-de-Gestion-de-Aranceles/UIHost/wwwroot/budget.png";

        if (File.Exists(logoPath))
        {
            _report.Logo = File.ReadAllBytes(logoPath);
        }
    }

    public void BuildTitle(string title)
    {
        _report.Title = title;
    }

    public void BuildData(List<Fee> data)
    {
        _report.Data = data.Cast<object>().ToList();
    }

    public void BuildChart()
    {
        // Placeholder for now — later you can set _report.Chart to a chart byte[]
        // Example for later:
        // _report.Chart = _chartGenerator.GenerateChartAsPng(data);
    }

    public void setChart(byte[] chart)
    {
        _report.Chart = chart;
    }

    public void BuildFooter()
    {
        _report.Footer = $"Reporte generado el {DateTime.Now:dd/MM/yyyy HH:mm}";
    }

    public Report GetReport()
    {
        _report.Result = GeneratePdf();
        return _report;
    }

    private byte[] GeneratePdf()
    {
        var pdfBytes = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
        }).GeneratePdf();

        return pdfBytes;
    }

    private void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            if (_report.Logo is byte[] logoBytes)
            {
                row.ConstantItem(60).Image(logoBytes);
            }

            row.RelativeItem().Column(col =>
            {
                col.Item().Text(_report.Title)
                    .FontSize(20)
                    .Bold()
                    .FontColor(Colors.Blue.Medium);

                col.Item().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}")
                    .FontSize(10)
                    .FontColor(Colors.Grey.Darken2);
            });
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.PaddingVertical(20).Column(col =>
        {
            // Table Header
            col.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(50);    // Año
                    columns.RelativeColumn(2);     // Descripción
                    columns.RelativeColumn(1);     // Monto
                    columns.RelativeColumn(1);     // Fecha límite
                    columns.RelativeColumn(1);     // Categoría
                });

                table.Header(header =>
                {
                    header.Cell().Text("Año").Bold();
                    header.Cell().Text("Descripción").Bold();
                    header.Cell().Text("Monto (Bs.)").Bold();
                    header.Cell().Text("Fecha Límite").Bold();
                    header.Cell().Text("Categoría").Bold();
                });

                foreach (Fee fee in _report.Data.Cast<Fee>())
                {
                    table.Cell().Text(fee.Year.ToString());
                    table.Cell().Text(fee.Description);
                    table.Cell().Text($"{fee.Amount:N2}");
                    table.Cell().Text(fee.DueDate.ToString("dd/MM/yyyy"));
                    table.Cell().Text(fee.Category?.Name ?? "—");
                }
            });

            col.Item().PaddingTop(25).Element(ComposeChartPlaceholder);
        });
    }

    private void ComposeChartPlaceholder(IContainer container)
    {
        // container.AlignCenter().Border(1).BorderColor(Colors.Grey.Lighten2).Height(150)
        //     .AlignMiddle()
        //     .Text("Gráfico (pendiente de implementación)")
        //     .FontColor(Colors.Grey.Darken1)
        //     .Italic();
    }

    private void ComposeFooter(IContainer container)
    {
        container.AlignRight().Text(_report.Footer)
            .FontSize(9)
            .FontColor(Colors.Grey.Darken1);
    }
}
