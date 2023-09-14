using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace ExtendPublic
{
    public static class ExtendDataRow
    {
        public static T RowToEnity<T>(this DataRow row, T enity = null) where T : class, new()
        {
            if (enity == null)
            {
                enity = new T();
            }
            PropertyInfo[] propertyInfos = enity.GetType().GetProperties();
            if (propertyInfos != null && propertyInfos.Any())
            {
                foreach (var item in propertyInfos)
                {
                    if (row.Table.Columns.Contains(item.Name))
                    {
                        item.SetValue(enity, Convert.ChangeType(row[item.Name], item.PropertyType));
                    }
                }
            }
            FieldInfo[] fieldInfos = enity.GetType().GetFields();
            foreach (var item in fieldInfos)
            {
                if (row.Table.Columns.Contains(item.Name))
                {
                    item.SetValue(enity, Convert.ChangeType(row[item.Name], item.FieldType));
                }
            }
            return enity;
        }

        public static DataRow EnityToDataRow<T>(this T enity) where T : class, new()
        {
            DataTable dt = new DataTable();
            if (enity == null)
            {
                return null;
            }
            PropertyInfo[] propertyInfos = enity.GetType().GetProperties();
            foreach (var item in propertyInfos)
            {
                dt.Columns.Add(item.Name);
            }
            DataRow row = dt.NewRow();
            foreach (var item in propertyInfos)
            {
                row[item.Name] = item.GetValue(enity)?.ToString();
            }
            return row;
        }
    }
}
