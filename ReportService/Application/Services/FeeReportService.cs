using System.Runtime.InteropServices.JavaScript;
using Common.Domain.SharedPorts;
using ReportService.Domain.Models;
using ReportService.Infrastructure.Builders;
using ReportService.Infrastructure.Directors;
using TariffingService.Domain.Models;
using TariffingService.Infrastructure.Adapters;

namespace ReportService.Application.Services;

public class FeeReportService
{
    private readonly IDbRepository<Fee> _feeRepository;
    private readonly IDbRepository<Category> _categoryRepository;

    public FeeReportService(IDbRepository<Fee> feeRepository, IDbRepository<Category> categoryRepository)
    {
        _feeRepository = feeRepository;
        _categoryRepository = categoryRepository;
    }

    public Report GeneratePdf(DateTime startDate, DateTime endDate, decimal minAmount, decimal maxAmount)
    {
        ReportDirector director = new ReportDirector();
        PdfReportBuilder builder = new PdfReportBuilder();

        List<Fee> data = _feeRepository.GetAll().Where(f => f.DueDate >= startDate && f.DueDate < endDate).Where(f => f.Amount >= minAmount && f.Amount <= maxAmount).ToList();
        
        foreach (var fee in data)
        {
            fee.Category = _categoryRepository.GetAll().FirstOrDefault(c => c.Id == fee.CategoryId);
        }

        return director.BuildPdfReport(builder, "Reporte de Aranaceles", data);
    }
}