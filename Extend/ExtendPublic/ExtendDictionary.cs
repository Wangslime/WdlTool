using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtendPublic
{
    public static class ExtendDictionary
    {
        const char crcs_split_1 = (char)1;
        const char crcs_split_2 = (char)18;
        const char crcs_split_3 = (char)17;
        const char crcs_split_4 = (char)15;

        public static bool GetValueExtend<T>(this ConcurrentDictionary<string, T> concurrentDic, string key, out T value)
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

        public static bool RemoveExtend<T>(this ConcurrentDictionary<string, T> concurrentDic, string key)
        {
            if (concurrentDic.ContainsKey(key))
            {
                return concurrentDic.TryRemove(key, out _);
            }

            return true;
        }


        public static bool GetValueExtend<T>(this Dictionary<string, T> concurrentDic, string key, out T value)
        {
            if (!concurrentDic.ContainsKey(key))
            {
                value = default;
                return false;
            }
            return concurrentDic.TryGetValue(key, out value);
        }

        public static bool AddOrUpdateExtend<T>(this Dictionary<string, T> concurrentDic, string key, T value)
        {
            if (!concurrentDic.ContainsKey(key))
            {
#if NET60
                return concurrentDic.TryAdd(key, value);
#else
                try
                {
                    concurrentDic.Add(key, value);
                    return true;
                }
                catch{}
                return false;
#endif
            }
            concurrentDic[key] = value;
            return true;
        }

        public static bool RemoveExtend<T>(this Dictionary<string, T> concurrentDic, string key)
        {
            if (concurrentDic.ContainsKey(key))
            {
                return concurrentDic.Remove(key);
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

        //public static Dictionary<string, string> DsStrToDic(this string strContent)
        //{
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    if (!string.IsNullOrEmpty(strContent))
        //    {
        //        var dtset = strContent.Split(crcs_split_4);
        //        foreach (var item in dtset)
        //        {
        //            var dtNameORData = item.Split(crcs_split_1);
        //            if (dtNameORData.Length >= 2)
        //            {
        //                var rowsArray = dtNameORData[1].Split(crcs_split_3);
        //                if (rowsArray.Length > 1)
        //                {
        //                    //标题行
        //                    var colRow = rowsArray[0];
        //                    var columnNamesArray = colRow.Split(crcs_split_2);
        //                    int rowCounts = columnNamesArray.Length;
        //                    //数据行
        //                    for (int i = 1; i < rowsArray.Length; i++)
        //                    {
        //                        if (rowsArray[i].Length > 0)
        //                        {
        //                            //一行数据
        //                            var colvalues = rowsArray[i].Split(crcs_split_2);
        //                            for (int colindex = 0; colindex < colvalues.Length && colindex < rowCounts; colindex++)
        //                            {
        //                                dic.Add(columnNamesArray[colindex], colvalues[colindex]);
        //                            }
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return dic;
        //}

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
