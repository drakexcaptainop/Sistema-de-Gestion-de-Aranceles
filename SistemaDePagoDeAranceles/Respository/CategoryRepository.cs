using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Factory;

namespace SistemaDePagoDeAranceles.Respository
{
    public class CategoryRepository : BaseDbRepository<Category>
    {
        public CategoryRepository(MySqlConnectionManager mySqlConnectionManager) : base (mySqlConnectionManager)
        {
            
        }
        public override int Delete(Category model)
        {
            string query = "DELETE FROM categoria WHERE id = @Id";
            return sqlConnectionManager.ExecuteParameterizedNonQuery<Category>(query, model);

        }

        public override IEnumerable<Category> GetAll()
        {
            string query = "Select nombre as Name, descripcion as Description, estadoLicenciaSanitaria as SanitaryLicenseStatus, fechaRegistro as DateRegister, ultimaActualizacion as LastUpdate, estado as Status from categoria";
            return sqlConnectionManager.ExecuteQuery<Category>(query);
        }

        public override int Insert(Category model)
        {
            string query = "INSERT INTO categoria (nombre, descripcion, estadoLicenciaSanitaria, fechaRegistro, ultimaActualizacion, estado) VALUES (@Name, @Description, @SanitaryLicenseStatus, @DateRegister, @LastUpdate, @Status)";
            return sqlConnectionManager.ExecuteParameterizedNonQuery<Category>(query, model);

        }

        public override int Update(Category model)
        {
            string query = "UPDATE categoria SET nombre = @Name, descripcion = @Description, estadoLicenciaSanitaria = @SanitaryLicenseStatus, fechaRegistro = @DateRegister, ultimaActualizacion = @LastUpdate, estado = @Status WHERE id = @Id";
            return sqlConnectionManager.ExecuteParameterizedNonQuery<Category>(query, model);
        }
    }
}
