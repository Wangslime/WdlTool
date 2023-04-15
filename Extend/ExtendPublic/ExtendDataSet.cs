using System.Collections.Generic;
using System.Collections;
using System;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;

namespace ExtendPublic
{
    public static class ExtendDataSet
    {
        #region 添加INTRESULT或者GSRESULT表
        public static DataSet GetIntResult(this DataSet ds, string tableName = "INTRESULT")
        {
            if (ds.IsDataSetEmpty()) return ds;
            if (ds.Tables.Contains(tableName))
            {
                ds.Tables.Remove(tableName);
            }
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add("0", "");
            ds.Tables.Add(dt);
            return ds;
        }
        public static DataSet GetIntResult(this DataSet ds, string msg, string tableName = "INTRESULT")
        {
            if (ds.IsDataSetEmpty()) return ds;
            if (ds.Tables.Contains(tableName))
            {
                ds.Tables.Remove(tableName);
            }
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add(string.IsNullOrEmpty(msg) ? "0" : "-1", msg);
            ds.Tables.Add(dt);
            return ds;
        }
        public static DataSet GetIntResult(this DataSet ds, string code, string msg, string tableName = "INTRESULT")
        {
            if (ds.IsDataSetEmpty()) return ds;
            if (ds.Tables.Contains(tableName))
            {
                ds.Tables.Remove(tableName);
            }
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add(code, msg);
            ds.Tables.Add(dt);
            return ds;
        }
        public static DataSet GetIntResult(this DataSet ds, long code, string msg, string tableName = "INTRESULT")
        {
            if (ds.IsDataSetEmpty()) return ds;
            if (ds.Tables.Contains(tableName))
            {
                ds.Tables.Remove(tableName);
            }
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add(code.ToString(), msg);
            ds.Tables.Add(dt);
            return ds;
        }



        public static DataSet GetGsResult(this DataSet ds, string tableName = "GSRESULT")
        {
            if (ds.IsDataSetEmpty()) return ds;
            if (ds.Tables.Contains(tableName))
            {
                ds.Tables.Remove(tableName);
            }
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add("0", "");
            ds.Tables.Add(dt);
            return ds;
        }
        public static DataSet GetGsResult(this DataSet ds, string msg, string tableName = "GSRESULT")
        {
            if (ds.IsDataSetEmpty()) return ds;
            if (ds.Tables.Contains(tableName))
            {
                ds.Tables.Remove(tableName);
            }
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add(string.IsNullOrEmpty(msg) ? "0" : "-1", msg);
            ds.Tables.Add(dt);
            return ds;
        }
        public static DataSet GetGsResult(this DataSet ds, string code, string msg, string tableName = "GSRESULT")
        {
            if (ds.IsDataSetEmpty()) return ds;
            if (ds.Tables.Contains(tableName))
            {
                ds.Tables.Remove(tableName);
            }
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add(code, msg);
            ds.Tables.Add(dt);
            return ds;
        }
        public static DataSet GetGsResult(this DataSet ds, long code, string msg, string tableName = "GSRESULT")
        {
            if (ds.IsDataSetEmpty()) return ds;
            if (ds.Tables.Contains(tableName))
            {
                ds.Tables.Remove(tableName);
            }
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            dt.Columns.Add("CODE");
            dt.Columns.Add("MSG");
            dt.Rows.Add(code.ToString(), msg);
            ds.Tables.Add(dt);
            return ds;
        }

        public static void GetGsResultDt(ref DataSet dsOut, string dtName = "GSRESULT")
        {
            string command = string.Empty;
            string guid = string.Empty;
            if (dsOut.Tables.Contains(dtName))
            {
                dsOut.Tables.Remove(dtName);
            }
            Dictionary<string, string> dicResult = new Dictionary<string, string>
            {
                { "CODE", "0" },
                { "MSG", "" },
                { "COMMAND", command },
                { "GUID", guid },
                { "TIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") }
            };
            DicToDataTable(ref dsOut, dtName, dicResult);
        }
        public static void GetGsResultDt(ref DataSet dsOut, DataSet dsIn, string code, string msg, string dtName = "GSRESULT")
        {
            string command = string.Empty;
            string guid = string.Empty;
            if (dsOut.Tables.Contains(dtName))
            {
                dsOut.Tables.Remove(dtName);
            }
            if (dsIn.Tables.Contains("GSCOMMAND"))
            {
                DataTable dt = dsIn.Tables["GSCOMMAND"];
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("COMMAND"))
                    {
                        command = dt.Rows[0]["COMMAND"].ToString();
                    }
                    if (dt.Columns.Contains("GUID"))
                    {
                        guid = dt.Rows[0]["GUID"].ToString();
                    }
                }
            }
            Dictionary<string, string> dicResult = new Dictionary<string, string>
            {
                { "CODE", code },
                { "MSG", msg },
                { "COMMAND", command },
                { "GUID", guid },
                { "TIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") }
            };
            DicToDataTable(ref dsOut, dtName, dicResult);
        }
        public static void GetGsResultDt(ref DataSet dsOut, string code, string msg, string dtName = "GSRESULT")
        {
            if (dsOut.Tables.Contains(dtName))
            {
                dsOut.Tables.Remove(dtName);
            }
            Dictionary<string, string> dicResult = new Dictionary<string, string>
            {
                { "CODE", code },
                { "MSG", msg },
                { "TIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") }
            };
            DicToDataTable(ref dsOut, dtName, dicResult);
        }
        public static void DicToDataTable(ref DataSet ds, string dtName, Dictionary<string, string> dic)
        {
            if (ds.Tables.Contains(dtName))
            {
                ds.Tables.Remove(dtName);
            }
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
            ds.Tables.Add(dt);
        }
        #endregion

        const char crcs_split_1 = (char)1;
        const char crcs_split_2 = (char)18;
        const char crcs_split_3 = (char)17;
        const char crcs_split_4 = (char)15;
        public static string DataSetToDsStr(this DataSet ds)
        {
            var sb = new StringBuilder();
            if (!ds.IsDataSetEmpty())
            {
                foreach (DataTable table in ds.Tables)
                {
                    //表名
                    sb.Append(table.TableName);
                    sb.Append(crcs_split_1);
                    //列
                    var strColumn = new StringBuilder();
                    foreach (DataColumn col in table.Columns)
                    {
                        strColumn.Append(col.ToString());
                        strColumn.Append(crcs_split_2);
                    }
                    strColumn.Append(crcs_split_3);
                    sb.Append(strColumn.ToString());
                    //数据行
                    foreach (DataRow row in table.Rows)
                    {
                        var strRowData = new StringBuilder();
                        foreach (var rowcolvalue in row.ItemArray)
                        {
                            strRowData.Append(rowcolvalue.ToString());
                            strRowData.Append(crcs_split_2);
                        }
                        //
                        strRowData.Append(crcs_split_3);
                        sb.Append(strRowData.ToString());
                    }
                    //表结尾符号
                    sb.Append(crcs_split_1);
                    sb.Append(crcs_split_4);
                }
            }
            return sb.ToString();
        }

        public static DataSet DsStrToDataSet(this string strContent)
        {
            var ds = new DataSet();
            if (!string.IsNullOrEmpty(strContent))
            {
                var dtset = strContent.Split(crcs_split_4);
                foreach (var item in dtset)
                {
                    //一个DataTable单元
                    var dtNameORData = item.Split(crcs_split_1);
                    if (dtNameORData.Length >= 2)
                    {
                        DataTable dstTableUnit = new DataTable()
                        {
                            TableName = dtNameORData[0]
                        };
                        var rowsArray = dtNameORData[1].Split(crcs_split_3);
                        if (rowsArray.Length > 1)
                        {
                            //标题行
                            var colRow = rowsArray[0];
                            var columnNamesArray = colRow.Split(crcs_split_2);
                            foreach (var columnName in columnNamesArray)
                            {
                                if (columnName.Length > 0)
                                {
                                    dstTableUnit.Columns.Add(columnName);
                                }
                            }
                            int rowCounts = dstTableUnit.Columns.Count;
                            //数据行
                            for (int i = 1; i < rowsArray.Length; i++)
                            {
                                if (rowsArray[i].Length > 0)
                                {
                                    //一行数据
                                    var colvalues = rowsArray[i].Split(crcs_split_2);
                                    var dtRow = dstTableUnit.NewRow();
                                    for (int colindex = 0; colindex < colvalues.Length && colindex < rowCounts; colindex++)
                                    {
                                        dtRow[colindex] = colvalues[colindex];
                                    }
                                    dstTableUnit.Rows.Add(dtRow);
                                }
                            }
                        }
                        ds.Tables.Add(dstTableUnit);
                    }
                }
            }
            return ds;
        }

        public static bool IsDataSetEmpty(this DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0)
            {
                return true;
            }
            foreach (DataTable table in ds.Tables)
            {
                if (table.Rows.Count > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static DataSet JsonToDataSet(this string jsonstring, bool isNeedToUpper = true)
        {
            if (string.IsNullOrEmpty(jsonstring))
            {
                return null;
            }
            if (isNeedToUpper)
            {
                jsonstring = jsonstring.ToUpper();
            }

            DataSet ds = new DataSet();
            // 查找结果集分隔符:{
            string sourcejson = jsonstring;
            while (!string.IsNullOrEmpty(sourcejson) && sourcejson.Length > 0)
            {
                //判断前面一个结果集的分隔符
                int iJudgePosA = sourcejson.IndexOf(":{");
                int iJudgePosB = sourcejson.IndexOf(":[");
                if (iJudgePosA < 0 && iJudgePosB < 0)
                {
                    break;
                }
                //第一个出现的结果集分隔符
                string strStart = null;
                string strEnd = null;
                #region 判断第一个结果集的分隔符
                if (Math.Min(iJudgePosA, iJudgePosB) < 0)
                {
                    //存在其中之一
                    if (iJudgePosA > iJudgePosB)
                    {
                        strStart = ":{";
                        strEnd = "}";
                    }
                    else
                    {
                        strStart = ":[";
                        strEnd = "]";
                    }
                }
                else
                {
                    //两种分隔符号都存在，取先出现的处理
                    if (iJudgePosA < iJudgePosB)
                    {
                        strStart = ":{";
                        strEnd = "}";
                    }
                    else
                    {
                        strStart = ":[";
                        strEnd = "]";
                    }
                }
                #endregion
                //截取第一个结果集，并将剩余值赋予sourcejson进行下次循环处理
                #region 第一个结果集解析处理
                {
                    int iRetIndex = sourcejson.IndexOf(strStart);
                    if (iRetIndex > 0 && (iRetIndex + strStart.Length) < sourcejson.Length)
                    {
                        //结果集名称，包含引号
                        string tableNamestring = sourcejson.Substring(0, iRetIndex);
                        //结果集值序列
                        string stringLeft = sourcejson.Substring(iRetIndex + strStart.Length, sourcejson.Length - iRetIndex - strStart.Length);
                        int iTableValueEnd = stringLeft.IndexOf(strEnd);
                        //"{\"YARDCONTAINERS\":[],\"INTRESULT\":{\"MSG\":\"\",\"CODE\":\"0\"}}"
                        //当碰到特殊情况，即表名YARDCONTAINERS后面直接是[]，则iTableValueEnd等于0，需继续解析，不能直接丢掉空表名
                        if (iTableValueEnd >= 0)
                        {
                            //截取第一个结果集后的剩余字符串，下一轮循环解析使用
                            if ((iTableValueEnd + 1) < stringLeft.Length)
                            {
                                sourcejson = stringLeft.Substring(iTableValueEnd + 1);
                            }
                            else
                            {
                                sourcejson = null;
                            }
                            //本次处理结果集字符串
                            string tablevaluestring = stringLeft.Substring(0, iTableValueEnd);
                            //获取匹配的结束位置
                            DataTable dt = new DataTable();
                            #region 获取结果集名称 tableNamestring
                            {
                                //包含结果集分隔对象，取出结果集
                                int iEnd = tableNamestring.LastIndexOf('"');
                                if (iEnd > 0)
                                {
                                    int iStart = tableNamestring.Substring(0, iEnd).LastIndexOf('"');
                                    if ((iStart + 1) < iEnd)
                                    {
                                        dt.TableName = tableNamestring.Substring(iStart + 1, iEnd - iStart - 1);
                                    }
                                }
                            }
                            #endregion
                            #region 获取结果集内容 tablevaluestring
                            {
                                Dictionary<string, string> coldc = new Dictionary<string, string>();
                                ArrayList arrvalues = new ArrayList();
                                //行分隔符 {
                                char[] spchar = { '{' };
                                string[] strRowData = tablevaluestring.Split(spchar, StringSplitOptions.RemoveEmptyEntries);
                                foreach (var strrow in strRowData)
                                {
                                    Dictionary<string, string> rowdatadc = new Dictionary<string, string>();
                                    //
                                    string strRow = strrow.Replace("}", string.Empty);
                                    string[] spcolumn = { "\"," };
                                    string[] strColumPair = strRow.Split(spcolumn, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (var strColUnit in strColumPair)
                                    {
                                        string[] spunitsplit = { "\":" };
                                        string[] strcolpairs = strColUnit.Split(spunitsplit, StringSplitOptions.None);
                                        if (strcolpairs.Length == 2)
                                        {
                                            strcolpairs[0] = strcolpairs[0].Replace("\"", string.Empty);
                                            strcolpairs[0] = strcolpairs[0].Replace(" ", string.Empty);
                                            strcolpairs[1] = strcolpairs[1].Replace("\"", string.Empty);
                                            strcolpairs[1] = strcolpairs[1].Replace(" ", string.Empty);
                                            //记录列名
                                            coldc[strcolpairs[0]] = strcolpairs[0];
                                            //记录值序列
                                            rowdatadc[strcolpairs[0]] = strcolpairs[1];
                                        }
                                    }
                                    //
                                    arrvalues.Add(rowdatadc);
                                }
                                //转化为DataTable
                                foreach (var colname in coldc)
                                {
                                    dt.Columns.Add(colname.Key);
                                }
                                foreach (Dictionary<string, string> rowunitdc in arrvalues)
                                {
                                    DataRow row = dt.NewRow();
                                    foreach (var valuepair in rowunitdc)
                                    {
                                        row[valuepair.Key] = valuepair.Value;
                                    }
                                    dt.Rows.Add(row);
                                }
                            }
                            #endregion
                            //
                            #region 解析结果转换存储
                            if (!dt.IsDataTableEmpty())
                            {
                                ds.Tables.Add(dt);
                            }
                            #endregion
                        }
                        else
                        {
                            //未找到结果集的结束标识,停止解析
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            #endregion
            return ds;
        }

        public static DataSet XML2DataSet(this string xmlData, bool bUseBase64)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                string strInputXML = xmlData;
                if (bUseBase64)
                {
                    strInputXML = Encoding.Default.GetString(System.Convert.FromBase64String(xmlData));
                }
                DataSet xmlDS = new DataSet();
                stream = new StringReader(strInputXML);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public static DataSet DataSetUpper(this DataSet dataSet)
        {
            foreach (DataTable dataTable in dataSet.Tables)
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
            }
            return dataSet;
        }
        public static DataSet DataSetCopyUpper(this DataSet dataSet)
        {
            dataSet = dataSet.Copy();
            foreach (DataTable dataTable in dataSet.Tables)
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
            }
            return dataSet;
        }

        public static string TryGetRowCell(this DataSet ds, string columnName, int iRowIndex = 0)
        {
            DataTable dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > iRowIndex && dt.Columns.Contains(columnName))
            {
                return dt.Rows[iRowIndex][columnName].ToString();
            }
            return "";
        }
    }
}
