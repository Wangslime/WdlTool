//using System.Collections.Generic;
//using System.Data;
//using System.Linq;

//namespace ExtendPublic
//{
//    public static class ExtendDataSetObj
//    {
//        public static DataSetObj DataSetToObj(this DataSet ds)
//        {
//            DataSetObj dataSetObj = null;
//            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
//            {
//                foreach (DataTable dt in ds.Tables)
//                {
//                    if (dt != null)
//                    {
//                        DatatableObj datatableObj = new DatatableObj();
//                        datatableObj.TableName = dt.TableName;
//                        if (dt != null)
//                        {
//                            foreach (DataColumn Column in dt.Columns)
//                            {
//                                List<string> list = new List<string>();
//                                foreach (DataRow Row in dt.Rows)
//                                {
//                                    if (Row[Column.ColumnName] == null)
//                                    {
//                                        list.Add("");
//                                    }
//                                    else
//                                    {
//                                        list.Add(Row[Column.ColumnName].ToString());
//                                    }

//                                }
//                                datatableObj?.Rows?.Add(Column.ColumnName, list);
//                                if (datatableObj?.RowCount == 0)
//                                {
//                                    datatableObj.RowCount = dt.Rows.Count;
//                                }
//                            }
//                        }
//                        if (dataSetObj == null)
//                        {
//                            dataSetObj = new DataSetObj();
//                        }
//                        dataSetObj?.DatatableObjs?.Add(datatableObj);
//                    }
//                }
//            }
//            return dataSetObj;
//        }

//        public static DataSet ObjToDataSet(this DataSetObj dataSetObj)
//        {
//            DataSet ds = new DataSet();
//            if (dataSetObj != null && dataSetObj.DatatableObjs != null && dataSetObj.DatatableObjs.Any())
//            {
//                foreach (DatatableObj datatableObj in dataSetObj.DatatableObjs)
//                {
//                    if (datatableObj != null)
//                    {
//                        DataTable dt = new DataTable(datatableObj.TableName);
//                        if (datatableObj.Rows != null && datatableObj.Rows.Any())
//                        {
//                            foreach (string columnName in datatableObj.Rows.Keys)
//                            {
//                                dt.Columns.Add(columnName);
//                            }
//                        }
//                        for (int i = 0; i < datatableObj.RowCount; i++)
//                        {
//                            DataRow dr = dt.NewRow();
//                            foreach (DataColumn colimn in dt.Columns)
//                            {
//                                if (datatableObj.Rows.ContainsKey(colimn.ColumnName))
//                                {
//                                    if (datatableObj.Rows[colimn.ColumnName].Count > i)
//                                    {
//                                        dr[colimn.ColumnName] = datatableObj.Rows[colimn.ColumnName][i];
//                                    }
//                                    else
//                                    {
//                                        dr[colimn.ColumnName] = "";
//                                    }
//                                }
//                                else
//                                {
//                                    dr[colimn.ColumnName] = "";
//                                }
//                            }
//                            dt.Rows.Add(dr);
//                        }
//                        ds.Tables.Add(dt);
//                    }
//                }
//            }
//            return ds;
//        }
//    }
//    public class DataSetObj
//    {
//        public List<DatatableObj> DatatableObjs { get; set; } = new List<DatatableObj>();
//    }

//    public class DatatableObj
//    {
//        public string TableName { get; set; } = "";
//        public int RowCount { get; set; } = 0;
//        public Dictionary<string, List<string>> Rows { get; set; } = new Dictionary<string, List<string>>();
//    }
//}