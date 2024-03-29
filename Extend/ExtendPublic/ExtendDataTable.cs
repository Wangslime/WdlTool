﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace ExtendPublic
{
    public static class ExtendDataTable
    {
        #region 添加INTRESULT或者GSRESULT表
        public static DataTable GetIntResult(this DataTable dt, string tableName = "INTRESULT")
        {
            if (dt == null) { dt = new DataTable(); }
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add("0", "");
            return dt;
        }
        public static DataTable GetIntResult(this DataTable dt, string msg, string tableName = "INTRESULT")
        {
            if (dt == null) { dt = new DataTable(); }
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add(string.IsNullOrEmpty(msg) ? "0" : "-1", msg);
            return dt;
        }
        public static DataTable GetIntResult(this DataTable dt, string code, string msg, string tableName = "INTRESULT")
        {
            if (dt == null) { dt = new DataTable(); }
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add(code, msg);
            return dt;
        }
        public static DataTable GetIntResult(this DataTable dt, long code, string msg, string tableName = "INTRESULT")
        {
            if (dt == null) { dt = new DataTable(); }
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add(code.ToString(), msg);
            return dt;
        }

        public static DataTable GetGsResult(this DataTable dt, string tableName = "GSRESULT")
        {
            if (dt == null) { dt = new DataTable(); }
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add("0", "");
            return dt;
        }
        public static DataTable GetGsResult(this DataTable dt, string msg, string tableName = "GSRESULT")
        {
            if (dt == null) { dt = new DataTable(); }
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add(string.IsNullOrEmpty(msg) ? "0" : "-1", msg);
            return dt;
        }
        public static DataTable GetGsResult(this DataTable dt, long code, string msg, string tableName = "GSRESULT")
        {
            if (dt == null) { dt = new DataTable(); }
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add(code, msg);
            return dt;
        }
        public static DataTable GetGsResult(this DataTable dt, int code, string msg, string tableName = "GSRESULT")
        {
            if (dt == null) { dt = new DataTable(); }
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add(code.ToString(), msg);
            return dt;
        }

        public static DataTable GsToDataTable(string code, string msg, string dtName = "GSRESULT")
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "CODE", code },
                { "MSG", msg },
                { "TIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") }
            };
            DataTable dt = new DataTable(dtName);
            foreach (var colName in dic.Keys)
            {
                dt.Columns.Add(colName, typeof(string));
            }
            DataRow dr = dt.NewRow();
            foreach (KeyValuePair<string, string> item in dic)
            {
                dr[item.Key] = item.Value;
            }
            dt.Rows.Add(dr);
            return dt;
        }
        #endregion

        public static DataTable SelectTable(this DataTable dt, string rowName, string value)
        {
            DataTable retDt = new DataTable(dt?.TableName);
            if (!dt.IsDataTableEmpty())
            {
                retDt = dt.Clone();
                if (dt.Columns.Contains(rowName))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = retDt.NewRow();
                        if (dt.Rows[i]?[rowName]?.ToString() == value)
                        {
                            row.ItemArray = dt.Rows[i].ItemArray;
                            retDt.Rows.Add(row);
                        }
                        else if (string.IsNullOrEmpty(value) && string.IsNullOrEmpty(dt.Rows[i][rowName].ToString()))
                        {
                            row.ItemArray = dt.Rows[i].ItemArray;
                            retDt.Rows.Add(row);
                        }
                    }
                }
            }
            return retDt;
        }

        public static DataTable SelectTable(this DataTable dt, string filterSql)
        {
            if (!dt.IsDataTableEmpty())
            {
                DataRow[] filteredRows = dt?.Select(filterSql);
                if (filteredRows == null || !filteredRows.Any())
                {
                    return dt = dt ?? new DataTable(dt.TableName);
                }
#if NET60
                if (filteredRows != null)
                {
                    DataTable dt1 = filteredRows.CopyToDataTable();
                    dt1.TableName = dt.TableName;
                    return dt1;
                }
                return null;
#else
                DataTable dt1= dt.Clone();
                foreach (DataRow row in filteredRows)
	            {
                    dt1.Rows.Add(row);
	            }
                return dt1;
#endif
            }
            return dt;
        }

        public static DataTable SelectTableValueNotNull(this DataTable dt, string rowName)
        {
            DataTable retDt = dt?.Clone();
            if (!dt.IsDataTableEmpty())
            {
                retDt.TableName = dt.TableName;
                retDt = dt.Clone();
                if (dt.Columns.Contains(rowName))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = retDt.NewRow();
                        if (!string.IsNullOrEmpty(dt.Rows[i][rowName].ToString()))
                        {
                            row.ItemArray = dt.Rows[i].ItemArray;
                            retDt.Rows.Add(row);
                        }
                    }
                }
            }
            return retDt;
        }

        public static bool IsDataTableEmpty(this DataTable dt)
        {
            if (dt == null || (dt != null && dt.Rows.Count == 0))
            {
                return true;
            }
            return false;
        }

        public static DataTable ListToDatatable<T>(this IEnumerable<T> enitys) where T : class, new()
        {
            if (enitys == null)
            {
                enitys = new List<T>();
            }
            T enity = enitys.FirstOrDefault();
            if (enity == null)
            {
                enity = new T();
            }
            DataTable dt = new DataTable(typeof(T).Name);
            PropertyInfo[] fieldInfos = enity.GetType().GetProperties();
            foreach (var fieldInfo in fieldInfos)
            {
                dt.Columns.Add(fieldInfo.Name);
            }
            foreach (var item in enitys)
            {
                DataRow row = dt.NewRow();
                fieldInfos = item.GetType().GetProperties();
                foreach (var fieldInfo in fieldInfos)
                {
                    row[fieldInfo.Name] = fieldInfo.GetValue(item)?.ToString();
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static string GetValueByColumnName(this DataTable dt, string columnName, int rowIndex = 0)
        {
            if (dt.IsDataTableEmpty()) return "";
            if (dt.Columns.Contains(columnName))
            {
                return dt.Rows[rowIndex][columnName].ToString();
            }
            return "";
        }

        public static string DataTable2XML(this DataTable xmlDS, bool bUseBase64)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {
                stream = new MemoryStream();
                writer = new XmlTextWriter(stream, Encoding.Default);
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UTF8Encoding utf = new UTF8Encoding();
                if (bUseBase64)
                {
                    string strEncode = utf.GetString(arr).Trim();
                    byte[] bt = Encoding.Default.GetBytes(strEncode);
                    return Convert.ToBase64String(bt);
                }
                else
                {
                    return utf.GetString(arr).Trim();
                }
            }
            catch
            {
                return String.Empty;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        public static DataTable DataTableUpper(this DataTable dataTable)
        {
            if (dataTable != null)
            {
                string upperTableName = dataTable.TableName.ToUpper();
                if (dataTable.TableName != upperTableName)
                {
                    dataTable.TableName = upperTableName;
                }
                foreach (DataColumn column in dataTable.Columns)
                {
                    string upperColumnName = column.ColumnName.ToUpper();
                    if (column.ColumnName != upperColumnName)
                    {
                        column.ColumnName = upperColumnName;
                    }
                }
            }
            return dataTable;
        }
        public static DataTable DataTableCopyUpper(this DataTable dataTable)
        {
            dataTable = dataTable.Copy();
            if (dataTable != null)
            {
                string upperTableName = dataTable.TableName.ToUpper();
                if (dataTable.TableName != upperTableName)
                {
                    dataTable.TableName = upperTableName;
                }
                foreach (DataColumn column in dataTable.Columns)
                {
                    string upperColumnName = column.ColumnName.ToUpper();
                    if (column.ColumnName != upperColumnName)
                    {
                        column.ColumnName = upperColumnName;
                    }
                }
            }
            return dataTable;
        }

        public static List<T> DataTableToList<T>(this DataTable dt) where T : class, new()
        {
            List<T> list = new List<T>();
            _ = string.Empty;
            foreach (DataRow row in dt.Rows)
            {
                T val = new T();
                val = row.RowToEnity(val);
                list.Add(val);
            }
            return list;
        }

        public static DataTable DicToDataTable(this Dictionary<string, string> dic, string dtName)
        {
            DataTable dt = new DataTable(dtName);
            foreach (var colName in dic.Keys)
            {
                dt.Columns.Add(colName, typeof(string));
            }
            DataRow dr = dt.NewRow();
            foreach (KeyValuePair<string, string> item in dic)
            {
                dr[item.Key] = item.Value;
            }
            dt.Rows.Add(dr);

            return dt;
        }

        public static string TryGetRowCell(this DataTable dt, string columnName, int iRowIndex = 0)
        {
            string value = "";
            if (dt != null && dt.Rows.Count > iRowIndex && dt.Columns.Contains(columnName))
            {
                value = dt.Rows[iRowIndex][columnName]?.ToString();
                if (string.IsNullOrEmpty(value))
                {
                    value = "";
                }
            }
            return value;
        }

        public static DataTable EnityToDataTable<T>(this T enity) where T : class, new()
        {
            if (enity == null)
            {
                enity = new T();
            }
            DataTable dt = new DataTable(typeof(T).Name);
            PropertyInfo[] fieldInfos = enity.GetType().GetProperties();
            foreach (var fieldInfo in fieldInfos)
            {
                dt.Columns.Add(fieldInfo.Name);
            }
            DataRow row = dt.NewRow();
            fieldInfos = enity.GetType().GetProperties();
            foreach (var fieldInfo in fieldInfos)
            {
                row[fieldInfo.Name] = fieldInfo.GetValue(enity)?.ToString();
            }
            dt.Rows.Add(row);
            return dt;
        }
    }
}
