using Microsoft.Extensions.Primitives;
using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Models;

namespace SistemaDePagoDeAranceles.Respository
{
    public class CategoriaRepository : BaseDbRepository<Categoria>
    {
        public CategoriaRepository(MySqlConnectionManager mySqlConnectionManager) : base(mySqlConnectionManager)
        {
        }
        public override IEnumerable<Categoria> GetAll()
        {
            string query = "Select nombre as Name, descripcion as Description, estadoLicenciaSanitaria as SanitaryLicenseState, fechaRegistro as DateRegister, ultimaActualizacion as LastUpdate, estado as Status from categoria";
            return sqlConnectionManager.ExecuteQuery<Categoria>(query);
        }

        public override int Delete(Categoria model)
        {
            string query = "DELETE FROM categoria WHERE id = @Id";
            return sqlConnectionManager.ExecuteParameterizedNonQuery<Categoria>(query, model);
        }

        public override int Insert(Categoria model)
        {
            string query = "INSERT INTO categoria (nombre, descripcion, estadoLicenciaSanitaria, fechaRegistro, ultimaActualizacion, estado) VALUES (@Name, @Description, @SanitaryLicenseState, @DateRegister, @LastUpdate, @Status)";
            return sqlConnectionManager.ExecuteParameterizedNonQuery<Categoria>(query, model);
        }

        public override int Update(Categoria model)
        {
            string query = "UPDATE categoria SET nombre = @Name, descripcion = @Description, estadoLicenciaSanitaria = @SanitaryLicenseState, fechaRegistro = @DateRegister, ultimaActualizacion = @LastUpdate, estado = @Status WHERE id = @Id";
            return sqlConnectionManager.ExecuteParameterizedNonQuery<Categoria>(query, model);
        }
    }
}
