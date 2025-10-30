using System.Runtime.CompilerServices;
using Common.Infrastructure.Persistence.Database;
using ReportService.Domain.Models;

namespace ReportService.Infrastructure.RepositoryAdapters;

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
}