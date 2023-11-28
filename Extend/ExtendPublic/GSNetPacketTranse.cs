using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ExtendPublic
{
    public class GSNetPacketTranse
    {
        const char crcs_split_1 = (char)1;
        const char crcs_split_2 = (char)18;
        const char crcs_split_3 = (char)17;
        const char crcs_split_4 = (char)15;

        /// <summary>
        /// 函数功能：dataset转字符串
        /// 作    者：林亚平
        /// 完成日期：2019-11-11
        /// </summary>
        /// <returns>string</returns>
        public static string TranseFromDataSet(DataSet ds)
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

        /// <summary>
        /// 函数功能：dictionary转字符串
        /// 作    者：林亚平
        /// 完成日期：2019-11-11
        /// </summary>
        /// <returns>string</returns>
        public static string TranseFromDataSet(Dictionary<string, string> dc)
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

        /// <summary>
        /// 函数功能：字符串转dataset
        /// 作    者：林亚平
        /// 完成日期：2019-11-11
        /// </summary>
        /// <returns>string</returns>
        public static DataSet TranseToDataSet(string strContent)
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
                                        //dtRow.SetField(colindex, colvalues[colindex]);
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
    }
}
