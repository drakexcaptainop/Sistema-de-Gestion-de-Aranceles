using Common.Domain.SharedPorts;
using EstablishmentService.Domain.Models;
using ReportService.Infrastructure.Directors;
using ReportService.Infrastructure.Builders;
using ReportService.Domain.Models;

public class EstablishmentReportService
{
    private readonly IDbRepository<Establishment> _establishmentRepo;
    private readonly IDbRepository<PersonInCharge> _personRepo;

    public EstablishmentReportService(IDbRepository<Establishment> establishmentRepo,
                                      IDbRepository<PersonInCharge> personRepo)
    {
        _establishmentRepo = establishmentRepo;
        _personRepo = personRepo;
    }

    public Report GenerateEncargadoReport(string exportedBy)
    {
        var establishments = _establishmentRepo.GetAll().ToList();
        var persons = _personRepo.GetAll().ToList();

        var grouped = establishments
            .GroupBy(e => e.PersonInChargeId)
            .Select(g =>
            {
                var person = persons.FirstOrDefault(p => p.Id == g.Key);
                var name = person != null ? $"{person.FirstName} {person.LastName}" : "Sin Encargado";
                var ci = person?.Ci ?? "—";

                return new
                {
                    EncargadoName = name,
                    Ci = ci,
                    Count = g.Count(),
                    Establishments = g.Select(e => new { e.Name, e.SanitaryLicense, e.Address }).ToList()
                };
            })
            .OrderByDescending(g => g.Count)
            .ToList();

        var data = new List<(string Encargado, string Ci, string Establecimiento, string Licencia, string Direccion)>();

        foreach (var group in grouped)
        {
            foreach (var est in group.Establishments)
            {
                data.Add((
                    group.EncargadoName,
                    group.Ci,
                    est.Name,
                    est.SanitaryLicense,
                    est.Address
                ));
            }
        }


        var builder = new XlsxReportBuilder();
        var director = new ReportDirector();
        builder.SetFooter($"Exportado por: {exportedBy} | Fecha/Hora: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

        var report = director.BuildXlsxReport(builder, "Reporte de Encargados y Establecimientos", data);
        return report;
    }

}
