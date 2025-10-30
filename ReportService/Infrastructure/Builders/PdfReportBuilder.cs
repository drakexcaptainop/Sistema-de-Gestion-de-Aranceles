using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using ReportService.Domain.Models;
using ReportService.Application.Interfaces;

namespace ReportService.Infrastructure.Builders
{
    public class PdfReportBuilder<T> : IReportBuilder<T>
    {
        private readonly Report _report = new();

        private Document? _document;

        public void BuildLogo()
        {
            _report.Logo = File.Exists("/home/archflony/RiderProjects/arqui/Sistema-de-Gestion-de-Aranceles/UIHost/wwwroot/budget.png")
                ? File.ReadAllBytes("/home/archflony/RiderProjects/arqui/Sistema-de-Gestion-de-Aranceles/UIHost/wwwroot/budget.png")
                : null;
        }

        public void BuildTitle(string title)
        {
            _report.Title = title;
        }

        public void BuildData(List<T> data)
        {
            // Store generic data (as objects) in the report
            _report.Data = data.Cast<object>().ToList();
        }

        public void BuildChart()
        {
            // Optional: generate or embed chart image
            // Example placeholder chart image
            if (File.Exists("wwwroot/images/sample-chart.png"))
                _report.Chart = File.ReadAllBytes("wwwroot/images/sample-chart.png");
        }

        public void BuildFooter()
        {
            _report.Footer = $"Reporte generado el {DateTime.Now:dd/MM/yyyy HH:mm}";
        }

        public Report GetReport()
        {
            // Compose the QuestPDF document
            _document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);

                    page.Header()
                        .AlignCenter()
                        .Element(ComposeHeader);

                    page.Content()
                        .PaddingVertical(20)
                        .Element(ComposeBody);

                    page.Footer()
                        .AlignCenter()
                        .Text(_report.Footer)
                        .FontSize(10)
                        .FontColor(Colors.Grey.Darken2);
                });
            });

            // Generate PDF bytes and store them
            _report.Result = _document.GeneratePdf();
            return _report;
        }

        // -------------------------
        // INTERNAL COMPOSITION LOGIC
        // -------------------------
        private void ComposeHeader(IContainer container)
        {
            container.Column(column =>
            {
                if (_report.Logo != null)
                {
                    column.Item().AlignCenter().Image(_report.Logo, ImageScaling.FitWidth);
                }

                column.Item().AlignCenter().PaddingTop(10).Text(_report.Title)
                    .FontSize(20)
                    .Bold()
                    .FontColor(Colors.Blue.Darken2);
            });
        }

        private void ComposeBody(IContainer container)
        {
            container.Column(column =>
            {
                // Data Table
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(3);
                    });

                    // Header
                    table.Header(header =>
                    {
                        header.Cell().Element(CellHeader).Text("Campo");
                        header.Cell().Element(CellHeader).Text("Valor");
                    });

                    // Rows
                    foreach (var item in _report.Data)
                    {
                        table.Cell().Element(CellBody).Text(item?.GetType().Name ?? "Item");
                        table.Cell().Element(CellBody).Text(item?.ToString() ?? "");
                    }

                    static IContainer CellHeader(IContainer container) =>
                        container.DefaultTextStyle(x => x.Bold().FontColor(Colors.Blue.Medium))
                                 .PaddingVertical(5)
                                 .BorderBottom(1)
                                 .BorderColor(Colors.Grey.Lighten2);

                    static IContainer CellBody(IContainer container) =>
                        container.PaddingVertical(3)
                                 .BorderBottom(0.5f)
                                 .BorderColor(Colors.Grey.Lighten3);
                });

                if (_report.Chart != null)
                {
                    column.Item().PaddingTop(20).AlignCenter().Image(_report.Chart, ImageScaling.FitWidth);
                }
            });
        }
    }
}
