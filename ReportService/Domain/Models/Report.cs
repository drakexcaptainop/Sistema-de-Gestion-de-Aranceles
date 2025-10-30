namespace ReportService.Domain.Models;

public class Report
{
    public object Logo { get; set; } = new();
    public string Title { get; set; } = "";
    public List<string> Data { get; set; } = new();
    public object Chart { get; set; } = new();
    public string Footer { get; set; } = "";
    public object Result { get; set; } = new();
}