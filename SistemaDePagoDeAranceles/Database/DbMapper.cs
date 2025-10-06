using System;
using System.Data;
using System.Reflection;

namespace SistemaDePagoDeAranceles.Database
{
    public static class DbMapper
    {
        public static T MapDataRowToModel<T>(DataRow row) where T : new()
        {
            T model = new();
            PropertyInfo[] propertyInfo = typeof(T).GetProperties();
            foreach (PropertyInfo property in propertyInfo)
            {
                //if (row.Table.Columns.Contains(property.Name))
                //{
                //    var castedValue = Convert.ChangeType(row[property.Name], property.PropertyType);
                //    property.SetValue(model, castedValue);
                //}
                if (row.Table.Columns.Contains(property.Name))
                {
                    var value = row[property.Name];

                    if (value == DBNull.Value)
                    {
                        // Manejar tipos anulables correctamente
                        if (IsNullable(property.PropertyType))
                        {
                            property.SetValue(model, null);
                        }
                        else if (property.PropertyType.IsValueType)
                        {
                            // Si es un valor tipo struct (int, bool, etc.), asignar su valor por defecto
                            property.SetValue(model, Activator.CreateInstance(property.PropertyType));
                        }
                        else
                        {
                            property.SetValue(model, null);
                        }
                    }
                    else
                    {
                        var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        var castedValue = Convert.ChangeType(value, targetType);
                        property.SetValue(model, castedValue);
                    }
                }
            }
            return model;
        }

        public static IEnumerable<T> MapDataTableToModelIterable<T>(DataTable dataTable) where T : new()
        {
            List<T> resultList = [];
            foreach (DataRow row in dataTable.Rows)
            {
                T mappedObject = MapDataRowToModel<T>(row);
                resultList.Add(mappedObject);
            }
            return resultList;
        }

        //
        private static bool IsNullable(Type type)
        {
            return !type.IsValueType || Nullable.GetUnderlyingType(type) != null;
        }
    }
}
