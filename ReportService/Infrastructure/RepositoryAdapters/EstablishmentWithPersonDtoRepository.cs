using System.Runtime.CompilerServices;
using Common.Infrastructure.Persistence.Database;
using ReportService.Domain.Models;

namespace ReportService.Infrastructure.RepositoryAdapters;

public class QueryParams
{
    public DateTime? StartDate { get; set; }
    public string? Type { get; set; }
}
public class EstablishmentWithPersonDtoRepository
{
    MySqlConnectionManager _dbConnectionManager;
    public EstablishmentWithPersonDtoRepository(MySqlConnectionManager dbConnectionManager)
    {
        _dbConnectionManager = dbConnectionManager;
    }
    public IEnumerable<EstablishmentWithPersonDto> GetAll()
    {
        string query = @"SELECT
                  COALESCE(CONCAT(p.first_name, ' ', p.last_name), 'Sin Encargado') AS Encargado,
                  COALESCE(p.ci, '—')                                              AS Ci,
                  e.name                                                           AS Establecimiento,
                  e.sanitary_license                                               AS Licencia,
                  e.address                                                        AS Direccion
                FROM establishment e
                LEFT JOIN person_in_charge p
                  ON p.id = e.person_in_charge_id
                ORDER BY Encargado, Establecimiento;";
        
        return _dbConnectionManager.ExecuteQuery<EstablishmentWithPersonDto>(query);
    }
    public IEnumerable<EstablishmentWithPersonDto> GetAllWithFilters(DateTime? startDate, string? type)
    {
        string query = @"
        SELECT
          COALESCE(CONCAT(p.first_name, ' ', p.last_name), 'Sin Encargado') AS Encargado,
          COALESCE(p.ci, '—')                                              AS Ci,
          e.name                                                           AS Establecimiento,
          e.sanitary_license                                               AS Licencia,
          e.address                                                        AS Direccion
        FROM establishment e
        LEFT JOIN person_in_charge p
          ON p.id = e.person_in_charge_id
        WHERE e.created_date >= @StartDate
          AND e.establishment_type = @Type
        ORDER BY Encargado, Establecimiento;";

        var parameters = new QueryParams()
        {
            StartDate = startDate,
            Type = type
        };

        return _dbConnectionManager.ExecuteParameterizedQuery<EstablishmentWithPersonDto, QueryParams>(query, parameters);
    }
}