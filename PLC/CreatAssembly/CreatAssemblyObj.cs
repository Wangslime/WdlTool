using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreatAssembly
{
    public class CreatAssemblyObj
    {
        //public string filePath = @"C:\Users\Lenovo\Documents\上下位参数 (自动保存的).xlsx";
        public TypeInfoName typeInfoName = null;

        Dictionary<string, List<ExcelInfo>> dicInfoList = new Dictionary<string, List<ExcelInfo>>();
        public async Task<bool> Creat(string fileExcelPath, string globalName, string assemblyName)
        {
            List<string> list = MiniExcel.GetSheetNames(fileExcelPath);

            dicInfoList.Clear();
            foreach (string s in list)
            {
                var info = await MiniExcel.QueryAsync<ExcelInfo>(fileExcelPath, s);
                if (!dicInfoList.ContainsKey(s))
                {
                    dicInfoList.Add(s, info.Where(p => !string.IsNullOrEmpty(p.Name)).ToList());
                }
            }

            var glovalList = await MiniExcel.QueryAsync<ExcelInfo>(fileExcelPath, globalName);
            glovalList = glovalList?.Where(p => !string.IsNullOrEmpty(p.Name));
            List<TypeInfoName> TypeNmaeList = new List<TypeInfoName>();
            if (glovalList != null && glovalList.Any())
            {
                //typeInfoName = GetTypeByTypeName("Global_Variables", "GlobalVariables", dicInfoList["Global_Variables"]);

                typeInfoName = GetTypeByTypeName(globalName, globalName, dicInfoList[globalName]);
                Dynamic.assemblyBuilder.Save(assemblyName);

                return true;
            }
            return false;
        }


        public TypeInfoName GetTypeByTypeName(string name, string strType, List<ExcelInfo> excelInfos)
        {
            TypeInfoName typeInfoName = null;
            Dictionary<string, dynamic> dicProperty = new Dictionary<string, dynamic>();
            foreach (ExcelInfo info in excelInfos)
            {
                Type type = null;
                string typeName = info.Type;
                typeName = typeName.Replace(";", "");
                typeName = typeName.Replace(":", "");
                typeName = typeName.Trim();
                if (dicInfoList.ContainsKey(typeName))
                {
                    TypeInfoName typeInfo = GetTypeByTypeName(info.Name, info.Type, dicInfoList[typeName]);
                    dicProperty.Add(typeInfo.Name, typeInfo.Value);
                }
                else
                {
                    if (typeName.Contains("ARRAY"))
                    {
                        int length = 0;
                        typeName = typeName.Replace("ARRAY", "");
                        typeName = typeName.Replace("OF", "");
                        typeName = typeName.Replace("..", "");
                        typeName = typeName.Replace("[", "");
                        typeName = typeName.Replace("]", "");
                        typeName = typeName.Trim();
                        if (typeName.Contains("BOOL"))
                        {
                            typeName = typeName.Replace("BOOL", "");
                            int.TryParse(typeName, out length);
                            type = typeof(bool[]);
                        }
                        else if (typeName.Contains("INT"))
                        {
                            typeName = typeName.Replace("INT", "");
                            int.TryParse(typeName, out length);
                            type = typeof(int[]);
                        }
                        else if (typeName.Contains("REAL"))
                        {
                            typeName = typeName.Replace("REAL", "");
                            int.TryParse(typeName, out length);
                            type = typeof(float[]);
                        }
                        else if (typeName.Contains("UDINT"))
                        {
                            typeName = typeName.Replace("UDINT", "");
                            int.TryParse(typeName, out length);
                            type = typeof(uint[]);
                        }
                        else if (typeName.Contains("STRING"))
                        {
                            typeName = typeName.Replace("STRING", "");
                            int.TryParse(typeName, out length);
                            type = typeof(string[]);
                        }
                        if (type != null)
                        {
                            length += 1;
                            dynamic value = Array.CreateInstance(type.GetElementType(), length);
                            dicProperty.Add(info.Name, value);
                        }
                    }
                    else
                    {
                        dynamic value = null;
                        if (typeName.Contains("BOOL"))
                        {
                            value = false;
                        }
                        else if (typeName.Contains("INT"))
                        {
                            value = 0;
                        }
                        else if (typeName.Contains("REAL"))
                        {
                            value = 0;
                        }
                        else if (typeName.Contains("UDINT"))
                        {
                            value = 0;
                        }
                        else if (typeName.Contains("STRING"))
                        {
                            value = string.Empty;
                        }
                        if (value != null)
                        {
                            dicProperty.Add(info.Name, value);
                        }
                    }
                }
            }
            if (dicProperty != null && dicProperty.Any())
            {
                strType = strType.Replace(";", "");
                strType = strType.Replace(":", "");
                strType = strType.Trim();
                Type type1 = dicProperty.GetDynamicType(strType);
                if (type1 != null)
                {
                    dynamic dyn = Activator.CreateInstance(type1);

                    if (dyn != null)
                    {
                        Type type2 = dyn.GetType();
                        foreach (var item in dicProperty)
                        {
                            System.Reflection.PropertyInfo propertyInfo = type2.GetProperty(item.Key);
                            propertyInfo.SetValue(dyn, item.Value);
                        }
                        typeInfoName = new TypeInfoName()
                        {
                            Name = name,
                            Value = dyn
                        };
                    }
                }
            }
            return typeInfoName;
        }
    }
    public class ExcelInfo
    {
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public string Description { get; set; } = "";
    }

    public class TypeInfoName
    {
        public string Name { get; set; } = "";
        public dynamic Value { get; set; }
    }
}
