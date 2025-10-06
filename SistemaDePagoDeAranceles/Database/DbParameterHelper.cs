using MySql.Data.MySqlClient;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SistemaDePagoDeAranceles.Database
{
    public static class DbParameterHelper
    {
        public static MySqlCommand PopulateCommandParameters<T>(string query, T model)
        {
            var parameters = Regex.Matches(query, @"@\w+");
            MySqlCommand command = new(query);
            PropertyInfo[] modelProperties = typeof(T).GetProperties();
            foreach (Match param in parameters)
            {
                string paramName = param.Value;
                string modelPropName = paramName[1..];
                PropertyInfo property = modelProperties.Where(p => p.Name == modelPropName).First();
                command.Parameters.Add(new MySqlParameter(paramName, DBNull.Value)).Value = property.GetValue(model);
            }
            return command;
        }
    }
}
