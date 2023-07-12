using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace ExtendPublic
{
    public static class ExtendDictionary
    {
        const char crcs_split_1 = (char)1;
        const char crcs_split_2 = (char)18;
        const char crcs_split_3 = (char)17;
        const char crcs_split_4 = (char)15;

        public static bool GetValue<T>(this ConcurrentDictionary<string, T> concurrentDic, string key, out T value)
        {
            if (!concurrentDic.ContainsKey(key))
            {
                value = default;
                return false;
            }

            return concurrentDic.TryGetValue(key, out value);
        }

        public static bool AddOrUpdateExtend<T>(this ConcurrentDictionary<string, T> concurrentDic, string key, T value)
        {
            if (!concurrentDic.ContainsKey(key))
            {
                return concurrentDic.TryAdd(key, value);
            }

            concurrentDic[key] = value;
            return true;
        }

        public static bool Remove<T>(this ConcurrentDictionary<string, T> concurrentDic, string key)
        {
            if (concurrentDic.ContainsKey(key))
            {
                return concurrentDic.TryRemove(key, out _);
            }

            return true;
        }


        public static string DicToDsStr(this Dictionary<string, string> dc)
        {
            var sb = new StringBuilder();
            if (dc != null && dc.Count > 0)
            {
                //表名
                sb.Append("DICTSET");
                sb.Append(crcs_split_1);
                var strColumn = new StringBuilder();
                var strRowData = new StringBuilder();
                foreach (string strDCKey in dc.Keys)
                {
                    //标题行
                    strColumn.Append(strDCKey);
                    strColumn.Append(crcs_split_2);
                    //数据行
                    strRowData.Append(dc[strDCKey]);
                    strRowData.Append(crcs_split_2);

                }
                //行结束符
                strColumn.Append(crcs_split_3);
                strRowData.Append(crcs_split_3);
                //添加行数据
                sb.Append(strColumn.ToString());
                sb.Append(strRowData.ToString());
                //表结尾符号
                sb.Append(crcs_split_1);
                sb.Append(crcs_split_4);
            }
            return sb.ToString();
        }

        public static Dictionary<string, string> EnityToDic<T>(this T enity) where T : class, new()
        {
            if (enity == null)
            {
                enity = new T();
            }
            PropertyInfo[] propertyInfos = enity.GetType().GetProperties();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in propertyInfos)
            {
                dic.Add(item.Name, item.GetValue(enity) != null ? item.GetValue(enity).ToString() : "");
            }
            return dic;
        }

        public static Dictionary<string, string> RowToDic(this DataRow row)
        {
            if (row == null)
            {
                return null;
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataColumn item in row.Table.Columns)
            {
                dic.Add(item.ColumnName, row[item.ColumnName]?.ToString());
            }
            return dic;
        }
    }
}
