namespace ReportService.Domain.Models;

public class Report
{
    public byte[]? Logo { get; set; }
    public string Title { get; set; } = "";
    public List<object> Data { get; set; } = new();
    public byte[]? Chart { get; set; }
    public string Footer { get; set; } = "";
    public byte[]? Result { get; set; }
}