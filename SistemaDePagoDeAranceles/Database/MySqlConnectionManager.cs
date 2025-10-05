using System.Data;
using MySql.Data.MySqlClient;

namespace SistemaDePagoDeAranceles.Database
{
    public class MySqlConnectionManager
    {
        private readonly MySqlConnection _connection;
        public MySqlConnectionManager(IConfiguration configuration)
        {
            _connection = new MySqlConnection(configuration.GetConnectionString("MySqlConnection"));
        }

        public IEnumerable<T> ExecuteQuery<T>(string query, T model) where T : new()
        {
            List<T> results = new();
            using MySqlCommand command = DbParameterHelper.PopulateCommandParameters(query, model);
            command.Connection = _connection;
            return ExecuteCommand<T>(command);
        }

        public IEnumerable<T> ExecuteQuery<T>(string query) where T : new()
        {
            MySqlCommand command = new(query, _connection);
            return ExecuteCommand<T>( command );
        }

        private IEnumerable<T> ExecuteCommand<T>(MySqlCommand command) where T: new()
        {
            _connection.Open();
            using MySqlDataReader reader = command.ExecuteReader();
            DataTable dataTable = new();
            dataTable.Load(reader);
            IEnumerable<T> results = DbMapper.MapDataTable<T>(dataTable);
            _connection.Close();
            return results;
        }
    }
}
