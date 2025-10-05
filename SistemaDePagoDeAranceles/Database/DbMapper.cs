using System;
using System.Data;
using System.Reflection;

namespace SistemaDePagoDeAranceles.Database
{
    public static class DbMapper
    {
        public static T MapDataRow<T>(DataRow row) where T : new()
        {
            T model = new();
            PropertyInfo[] propertyInfo = typeof(T).GetProperties();
            foreach (PropertyInfo property in propertyInfo)
            {
                if (row.Table.Columns.Contains(property.Name))
                {
                    var castedValue = Convert.ChangeType(row[property.Name], property.PropertyType);
                    property.SetValue(model, castedValue);
                }
            }
            return model;
        }

        public static IEnumerable<T> MapDataTable<T>(DataTable dataTable) where T : new()
        {
            List<T> resultList = [];
            foreach (DataRow row in dataTable.Rows)
            {
                T mappedObject = MapDataRow<T>(row);
                resultList.Add(mappedObject);
            }
            return resultList;
        }
    }
}
