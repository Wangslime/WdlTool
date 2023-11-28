using System;
using System.Data;
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
            foreach (var item in propertyInfos)
            {
                if (row.Table.Columns.Contains(item.Name))
                {
                    if (string.IsNullOrEmpty(row[item.Name]?.ToString()))
                    {
                        continue;
                    }
                    if (item.PropertyType == typeof(string))
                    {
                        item.SetValue(enity, row[item.Name]?.ToString());
                    }
                    else if (item.PropertyType == typeof(DateTime) || item.PropertyType == typeof(DateTime?))
                    {
                        if (DateTime.TryParse(row[item.Name].ToString(), out DateTime dValue))
                        {
                            item.SetValue(enity, dValue);
                        }
                    }
                    else if (item.PropertyType == typeof(long) || item.PropertyType == typeof(long?))
                    {
                        if (long.TryParse(row[item.Name].ToString(), out long lValue))
                        {
                            item.SetValue(enity, lValue);
                        }
                    }
                    else if (item.PropertyType == typeof(int) || item.PropertyType == typeof(int?))
                    {
                        if (int.TryParse(row[item.Name].ToString(), out int nValue))
                        {
                            item.SetValue(enity, nValue);
                        }
                    }
                    else if (item.PropertyType == typeof(char) || item.PropertyType == typeof(char?))
                    {
                        if (char.TryParse(row[item.Name].ToString(), out char cValue))
                        {
                            item.SetValue(enity, cValue);
                        }
                    }
                    else if (item.PropertyType == typeof(double) || item.PropertyType == typeof(double?))
                    {
                        if (double.TryParse(row[item.Name].ToString(), out double dValue))
                        {
                            item.SetValue(enity, dValue);
                        }
                    }
                    else if (item.PropertyType == typeof(float) || item.PropertyType == typeof(float?))
                    {
                        if (float.TryParse(row[item.Name].ToString(), out float fValue))
                        {
                            item.SetValue(enity, fValue);
                        }
                    }
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

        public static void DataRowToEntity(this DataRow row, object entity)
        {
            if (row == null || entity == null)
            {
                return;
            }
            MemberInfo[] members = entity.GetType().GetMembers();
            foreach (MemberInfo memberInfo in members)
            {
                if (!(memberInfo.Name == "_DB_TABLE_NAME_") && !(memberInfo.Name == "_DB_PK_COLLUMN_NAME_"))
                {
                    string text = memberInfo.Name.ToUpper();
                    if (row.Table.Columns.Contains(text))
                    {
                        object obj = null;
                        obj = ((row.RowState != DataRowState.Deleted) ? row[text] : row[text, DataRowVersion.Original]);
                        entity.SetMemberValue(memberInfo, obj);
                    }
                }
            }
        }
        public static void SetMemberValue(this object entity, MemberInfo member, object value)
        {
            if ((entity == null) || (member == null))
            {
                return;
            }

            Type type = member.GetMemberValueType();

            object newValue;
            if (DBNull.Value.Equals(value) || (value == null) || string.IsNullOrEmpty(value.ToString()))
            {
                newValue = type.GetDefaultValue();
            }
            else if (type.Name == "Boolean" && value.GetType().Name == "String")
            {
                newValue = "TRUE".Equals(value.ToString().ToUpper()) ? true : false;
            }
            else
            {
                newValue = Convert.ChangeType(value, type);
            }

            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    FieldInfo field = member as FieldInfo;
                    if (field != null)
                    {
                        field.SetValue(entity, newValue);
                    }

                    break;
                case MemberTypes.Property:
                    PropertyInfo property = member as PropertyInfo;
                    if (property != null)
                    {
                        property.SetValue(entity, newValue, null);
                    }

                    break;
                default:
                    break;
            }
        }

        public static object GetDefaultValue(this Type type)
        {
            if (type.Equals(typeof(string)))
            {
                return null;
            }

            if (type.Equals(typeof(bool)))
            {
                return false;
            }

            FieldInfo minField = type.GetField("MinValue");
            if (minField != null)
            {
                return minField.GetValue(null);
            }

            return null;
        }

        public static Type GetMemberValueType(this MemberInfo member)
        {
            if (member == null)
            {
                return null;
            }

            Type type = null;

            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    FieldInfo field = member as FieldInfo;
                    if (field != null)
                    {
                        type = field.FieldType;
                    }

                    break;
                case MemberTypes.Property:
                    PropertyInfo property = member as PropertyInfo;
                    if (property != null)
                    {
                        type = property.PropertyType;
                    }

                    break;
                default:
                    break;
            }

            return type;
        }

        public static T[] CombinArray<T>(T[] array1, T[] array2)
        {
            if (array1 == null)
            {
                return array2;
            }

            if (array2 == null)
            {
                return array1;
            }

            T[] combinedArray = new T[array1.Length + array2.Length];

            int index = 0;
            foreach (T t in array1)
            {
                combinedArray[index] = t;
                index++;
            }

            foreach (T t in array2)
            {
                combinedArray[index] = t;
                index++;
            }

            return combinedArray;
        }
    }
}
