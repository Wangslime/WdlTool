﻿using Microsoft.CodeAnalysis;
using MiniExcelLibs;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;

namespace Drsoft.Tools.DynamicBuilder
{
    public class CreatAssemblyObj
    {
        private string filePath = "";
        private TypeInfoName? typeInfoName = null;
        private Dictionary<string, List<ExcelInfo>> dicInfoList = new Dictionary<string, List<ExcelInfo>>();
        private Dictionary<string, dynamic> dicPropertyValue = new Dictionary<string, dynamic>();
        private string AssemblyName = "";
        public async Task<(bool, string)> StartCreatAssembly(ConfigJsonInfo config, string saveAssemblyName = "MyDynamicAssembly")
        {
            string filePath = config.LoadExcelPath;
            string globalDataName = config.GlobalDataName;
            string classFileSavePath = config.ClassFileSavePath;

            if (!string.IsNullOrEmpty(config.CreatAssemblyName))
            {
                saveAssemblyName = config.CreatAssemblyName;
            }

            if (string.IsNullOrEmpty(filePath))
            {
                return (false, "Excel文件路径不能为空");
            }
            if (string.IsNullOrEmpty(globalDataName))
            {
                return (false, "与下位机交互主定义界面Sheet不能为空");
            }

            if (!filePath.Contains(@":\"))
            {
                filePath = AppContext.BaseDirectory + filePath;
            }
            if (string.IsNullOrEmpty(classFileSavePath))
            {
                classFileSavePath = AppContext.BaseDirectory + globalDataName;
            }

            this.AssemblyName = saveAssemblyName;
            try
            {
                this.filePath = filePath;
                List<string> list = MiniExcel.GetSheetNames(filePath);

                dicInfoList.Clear();
                foreach (string s in list)
                {
                    IEnumerable<ExcelInfo> info = await MiniExcel.QueryAsync<ExcelInfo>(filePath, s);

                    foreach (ExcelInfo item in info)
                    {
                        if (item.Name == "Reserve")
                        {
                            item.Name = "Reserve" + Guid.NewGuid();
                        }
                    }
                    if (!dicInfoList.ContainsKey(s))
                    {
                        dicInfoList.TryAdd(s, info.Where(p => !string.IsNullOrEmpty(p.Name)).ToList());
                    }
                }
                var glovalList = dicInfoList[globalDataName];

                foreach (var item in glovalList)
                {
                    string typeName = item.Type;
                    typeName = typeName.Replace(";", "");
                    typeName = typeName.Replace(":", "");
                    typeName = typeName.Trim();
                    if (!list.Contains(typeName) && typeName.Contains("[") && typeName.Contains("]"))
                    {
                        dicInfoList.Add(item.Name + "Class", new List<ExcelInfo>() { new ExcelInfo() { Name = item.Name, Type = item.Type, Description = item.Description } });
                        item.Type = item.Name + "Class";
                        dicInfoList.Remove(item.Name);
                    }
                }
                if (glovalList != null && glovalList.Any())
                {
                    //根据Excel生成动态程序集，包括各种类和属性对象
                    typeInfoName = GetTypeByTypeName("", globalDataName, dicInfoList[globalDataName]);
                    dicPropertyValue.Add(globalDataName, typeInfoName.Value);
                }

                Type[] types = DynamicAssembly.assemblyBuilder.GetTypes();
                if (classFileSavePath.EndsWith(".cs"))
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (Type type in types)
                    {
                        string plcCode = GenerateCodeFromAssembly(DynamicAssembly.assemblyBuilder, type);
                        sb.Append(plcCode);
                        sb.Append('\n'); 
                        sb.Append('\n');
                    }
                    string file1 = classFileSavePath;
                    if (File.Exists(file1))
                    {
                        File.Delete(file1);
                    }
                    await File.WriteAllTextAsync(file1, sb.ToString(), Encoding.UTF8);
                }
                else
                {
                    foreach (Type type in types)
                    {
                        string plcCode = GenerateCodeFromAssembly(DynamicAssembly.assemblyBuilder, type);

                        string file1 = classFileSavePath + "\\" + type.Name + ".cs";
                        if (File.Exists(file1))
                        {
                            File.Delete(file1);
                        }
                        await File.WriteAllTextAsync(file1, plcCode, Encoding.UTF8);
                    }
                }

                //classFileSavePath
                //assemblySavePath

                

                return (true,"");
                //string name = DynamicAssembly.assemblyBuilder.GetName().Name;

                ////C# 字符串代码生成程序集实体程序集
                //var compilation = CSharpCompilation.Create(name)
                //                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                //                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                //                .AddSyntaxTrees(CSharpSyntaxTree.ParseText(code));


                //if (string.IsNullOrEmpty(assemblySavePath))
                //{
                //    assemblySavePath = AppContext.BaseDirectory;
                //}
                //string file2 = $"{assemblySavePath}{saveAssemblyName}.dll";
                //if (File.Exists(file2))
                //{
                //    File.Delete(file2);
                //}
                //string file3 = $"{assemblySavePath}{saveAssemblyName}.pdb";
                //if (File.Exists(file3))
                //{
                //    File.Delete(file3);
                //}
                //var result = compilation.Emit($"{assemblySavePath}{saveAssemblyName}.dll", $"{assemblySavePath}{saveAssemblyName}.pdb");
                //string msg = "";
                //foreach (var item in result.Diagnostics)
                //{
                //    msg += item.GetMessage();
                //}

                //return (result.Success, msg);
            }
            catch (Exception ex)
            {
                string msg = $"{ex.Message},{ex.StackTrace}";
                return (false, msg);
            }
        }

        /// <summary>
        /// 生成DrMark程序文件代码
        /// </summary>
        /// <param name="drMarkExcelPath"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task<string> GenerateDrMarkCodeFromAssembly(string drMarkExcelPath)
        {
            List<string> methodNameList = new List<string>();
            var excleQuery = await MiniExcel.QueryAsync<DrMarkExcel>(drMarkExcelPath);
            if (excleQuery != null && excleQuery.Any())
            {
                foreach (var item in excleQuery)
                {
                    if (!string.IsNullOrEmpty(item.MethodBody))
                    {
                        int index1 = item.MethodBody.IndexOf('(');
                        string name = item.MethodBody.Substring(0, index1);
                        methodNameList.Add(name);
                    }
                }
            }


            StringBuilder sb = new StringBuilder();
            sb.Append("using System.Reflection;");
            sb.Append('\n');
            sb.Append($"namespace Drsoft.DrMark");
            sb.Append('\n');
            sb.Append('{');
            sb.Append('\n');
            sb.Append("public partial class DrMarkAdaptor");
            sb.Append('\n');
            sb.Append('{');
            sb.Append('\n');

            foreach (string name in methodNameList)
            {
                //public double DynamicMethod(int num, params object[] objs)
                //{
                //    string methodName = MethodBase.GetCurrentMethod().Name;
                //    return plugins[num - 1].DrMarkMethod(methodName, objs);
                //}

                //public double DynamicMethod(params object[] objs)
                //{
                //    string methodName = MethodBase.GetCurrentMethod().Name;
                //    dynamic ret = 0d;
                //    foreach (DrMarkPlugin drMarkPlugin in plugins)
                //    {
                //        ret += drMarkPlugin.DrMarkMethod(methodName, objs);
                //    }
                //    return ret;
                //}
                sb.Append($"public double {name}(int num, params object[] objs)");
                sb.Append('\n');
                sb.Append('{');
                sb.Append('\n');
                sb.Append("string methodName = MethodBase.GetCurrentMethod().Name;");
                sb.Append('\n');
                sb.Append("return plugins[num - 1].DrMarkMethod(methodName, objs);");
                sb.Append('\n');
                sb.Append('}');
                sb.Append('\n');

                //public double DynamicMethod(params object[] objs)
                //{
                //    string methodName = MethodBase.GetCurrentMethod().Name;
                //    dynamic ret = 0d;
                //    foreach (DrMarkPlugin drMarkPlugin in plugins)
                //    {
                //        ret += drMarkPlugin.DrMarkMethod(methodName, objs);
                //    }
                //    return ret;
                //}
                sb.Append($"public double {name}(params object[] objs)");
                sb.Append('\n');
                sb.Append('{');
                sb.Append('\n');
                sb.Append("string methodName = MethodBase.GetCurrentMethod().Name;");
                sb.Append('\n');
                sb.Append("dynamic ret = 0d;");
                sb.Append('\n');
                sb.Append("foreach (DrMarkPlugin drMarkPlugin in plugins)");
                sb.Append('\n');
                sb.Append('{');
                sb.Append('\n');
                sb.Append("ret += drMarkPlugin.DrMarkMethod(methodName, objs);");
                sb.Append('\n');
                sb.Append('}');
                sb.Append('\n');
                sb.Append("return ret;");
                sb.Append('\n');
                sb.Append('}');
            }

            sb.Append('\n');
            sb.Append('}');

            sb.Append('\n');
            sb.Append('}');

            return sb.ToString();
        }
        public class DrMarkExcel
        {
            public string MethodBody { get; set; }
            public string Description { get; set; }
        }

        /// <summary>
        /// 将动态程序集转换为C#字符串代码
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private string GenerateCodeFromAssembly(AssemblyBuilder assembly, Type type)
        {

            #region 命名控件内

            StringBuilder sb = new StringBuilder();
            sb.Append("using System.ComponentModel;");
            sb.Append('\n');
            sb.Append("using System.Runtime.InteropServices;");
            sb.Append('\n');
            sb.Append($"namespace Drsoft.PLC.Modle");
            sb.Append('\n');
            sb.Append('{');
            sb.Append('\n');
            if (dicPropertyValue.ContainsKey(type.Name))
            {
                object obj = dicPropertyValue[type.Name];
                sb.Append("[StructLayout(LayoutKind.Sequential, Pack = 1)]");
                sb.Append('\n');
                sb.Append($"public class {type.Name}");
                sb.Append('\n');
                sb.Append('{');
                sb.Append('\n');
                #region 类里面
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (PropertyInfo property in propertyInfos)
                {
                    DescriptionAttribute? plcAttribute = property.GetCustomAttribute<DescriptionAttribute>();
                    string? description = plcAttribute.Description;
                    //string? description = dicInfoList[type.Name].FirstOrDefault(p => p.Name == property.Name)?.Description;
                    sb.Append("/// <summary>");
                    sb.Append('\n');
                    sb.Append("/// ");
                    sb.Append(description);
                    sb.Append('\n');
                    sb.Append("/// </summary>");
                    sb.Append('\n');


                    sb.Append(@$"[Description(""{description}"")]");
                    dynamic obj1 = property.GetValue(obj);
                    string Unmanaged = GetUnmanagedType(obj1);
                    sb.Append('\n');
                    if (!string.IsNullOrEmpty(Unmanaged))
                    {
                        sb.Append(Unmanaged);
                        sb.Append('\n');
                    }
                    sb.Append($"public {property.PropertyType.FullName} {property.Name}");
                    sb.Append("{get; set;}");
                    sb.Append($"={GetDefaultValue(property.PropertyType, obj1)};");
                    sb.Append('\n');
                }

                sb.Append('\n');
                sb.Append("public override bool Equals(object? obj)");
                sb.Append('\n');
                sb.Append('{');
                sb.Append('\n');
                sb.Append("if (obj == null) return false;");
                sb.Append('\n');
                sb.Append($"if (obj is not {type.Name}) return false;");
                sb.Append('\n');
                sb.Append($"{type.Name} typeObj = ({type.Name})obj;");
                foreach (PropertyInfo property in propertyInfos)
                {
                    sb.Append('\n');
                    sb.Append($"if (!this.{property.Name}.Equals(typeObj.{property.Name}))");
                    sb.Append('\n');
                    sb.Append('{');
                    sb.Append('\n');
                    sb.Append("return false;");
                    sb.Append('\n');
                    sb.Append('}');
                }
                sb.Append('\n');
                sb.Append("return true;");
                sb.Append('\n');
                sb.Append('}');

                sb.Append('\n');
                sb.Append("public override string ToString()");
                sb.Append('\n');
                
                sb.Append('{');
                sb.Append('\n');
                sb.Append($"return ");
                sb.Append('$');
                sb.Append("\"");
                sb.Append($"{type.Name}:");
                sb.Append('{');
                sb.Append('{');
                int length = propertyInfos.Length;
                int i = 0;
                foreach (PropertyInfo property in propertyInfos)
                {
                    i++;
                    sb.Append($"{property.Name} = ");
                    sb.Append('{');
                    sb.Append($"{property.Name}");
                    sb.Append('}');
                    if (i != length)
                    {
                        sb.Append($", ");
                    }
                }
                sb.Append('}');
                sb.Append('}');
                sb.Append("\";");
                sb.Append('\n');
                sb.Append('}');
                sb.Append('\n');

                #endregion
                sb.Append('}');
                sb.Append('\n');

            }
            sb.Append('}');

            return sb.ToString();
            #endregion
        }

        /// <summary>
        /// 获取特性的字符串格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string GetUnmanagedType(dynamic obj)
        {
            Type type = obj.GetType();
            string unmanagedType = "";
            if (type.IsArray)
            {
                int length = obj.Length;
                UnmanagedType uType = UnmanagedType.Error;
                if (type == typeof(bool[]))
                {
                    uType = UnmanagedType.I1;
                }
                else if (type == typeof(short[]) || type == typeof(ushort[]))
                {
                    uType = UnmanagedType.U2;
                }
                else if (type == typeof(uint[]) || type == typeof(int[]))
                {
                    uType = UnmanagedType.U4;
                }
                else if (type == typeof(ulong[]) || type == typeof(long[]))
                {
                    uType = UnmanagedType.U8;
                }
                else if (type == typeof(float[]))
                {
                    uType = UnmanagedType.R4;
                }
                else if (type == typeof(double[]))
                {
                    uType = UnmanagedType.R8;
                }
                else if (type == typeof(char[]))
                {
                    uType = UnmanagedType.BStr;
                }
                else if (type == typeof(string[]))
                {
                    uType = UnmanagedType.LPStr;
                }

                if (uType != UnmanagedType.Error)
                {
                    unmanagedType = $"[field: MarshalAs(UnmanagedType.ByValArray, SizeConst = {length}, ArraySubType = UnmanagedType.{uType})]";
                }
            }
            else
            {
                UnmanagedType uType = UnmanagedType.Error;
                if (type == typeof(bool))
                {
                    uType = UnmanagedType.I1;
                }
                else if (type == typeof(short) || type == typeof(ushort))
                {
                    uType = UnmanagedType.U2;
                }
                else if (type == typeof(uint) || type == typeof(int))
                {
                    uType = UnmanagedType.U4;
                }
                else if (type == typeof(ulong) || type == typeof(long))
                {
                    uType = UnmanagedType.U8;
                }
                else if (type == typeof(float))
                {
                    uType = UnmanagedType.R4;
                }
                else if (type == typeof(double))
                {
                    uType = UnmanagedType.R8;
                }
                else if (type == typeof(string))
                {
                    uType = UnmanagedType.LPStr;
                }
                if (uType != UnmanagedType.Error)
                {
                    unmanagedType = $"[field: MarshalAs(UnmanagedType.{uType})]";
                }
            }
            return unmanagedType;
        }

        /// <summary>
        /// 获取属性值的字符串格式
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string GetDefaultValue(Type type, dynamic obj)
        {
            if (type.IsArray)
            {
                type = type.GetElementType();
                return $"new {type.FullName}[{obj.Length}]";
            }
            else if (type == typeof(string))
            {
                if (string.IsNullOrEmpty(obj))
                {
                    return @"""""";
                }
                else
                {
                    return @$"""{obj.ToString()}""";
                }
            }
            else if (type.IsClass)
            {
                return $"new {type.Name}()";
            }
            else if (type == typeof(bool))
            {
                return obj.ToString().ToLower();
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// 根据Excel生成动态程序集
        /// 递归调用
        /// 包括各种类和属性对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="strType"></param>
        /// <param name="excelInfos"></param>
        /// <returns></returns>
        private TypeInfoName? GetTypeByTypeName(string name, string strType, List<ExcelInfo> excelInfos)
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
                    TypeInfoName? typeInfo = GetTypeByTypeName(info.Name, info.Type, dicInfoList[typeName]);
                    if (typeInfo != null)
                    {
                        dicProperty.Add(typeInfo.Name, typeInfo.Value);
                        string name1 = typeInfo.Value.GetType().Name;
                        if (!dicPropertyValue.ContainsKey(name1))
                        {
                            dicPropertyValue.Add(name1, typeInfo.Value);
                        }
                        //string globName;
                        //if (!string.IsNullOrEmpty(name))
                        //{
                        //    globName = $"{name}.{info.Name}";
                        //}
                        //else
                        //{
                        //    globName = info.Name;
                        //}
                    }
                }
                else
                {
                    if (typeName.Contains("ARRAY"))
                    {
                        int length = 0;
                        string strLength = typeName.Substring(typeName.IndexOf('[') + 1, typeName.IndexOf(']') - typeName.IndexOf('[') - 1);
                        typeName = typeName.Replace("ARRAY", "");
                        typeName = typeName.Replace("OF", "");
                        typeName = typeName.Replace("..", "");
                        typeName = typeName.Replace("[", "");
                        typeName = typeName.Replace("]", "");
                        typeName = typeName.Trim();
                        length = int.Parse(strLength.Split('.')[2]);
                        length++;
                        if (typeName.Contains("BOOL"))
                        {
                            type = typeof(bool[]);
                        }
                        else if (typeName.Contains("UINT"))
                        {
                            type = typeof(ushort[]);
                        }
                        else if (typeName.Contains("UDINT"))
                        {
                            type = typeof(uint[]);
                        }
                        else if (typeName.Contains("DINT"))
                        {
                            type = typeof(int[]);
                        }
                        else if (typeName.Contains("INT"))
                        {
                            type = typeof(short[]);
                        }
                        else if (typeName.Contains("LREAL"))
                        {
                            type = typeof(double[]);
                        }
                        else if (typeName.Contains("REAL"))
                        {
                            type = typeof(float[]);
                        }
                        else if (typeName.Contains("STRING"))
                        {
                            type = typeof(string[]);
                        }
                        if (type != null)
                        {
                            // 给数组类型赋初始值
                            dynamic value = Array.CreateInstance(type.GetElementType(), length);
                            dicProperty.Add(info.Name, value);
                        }
                    }
                    else if (typeName.Contains("STRING"))
                    {
                        typeName = typeName.Replace("(", "");
                        typeName = typeName.Replace(")", "");
                        typeName = typeName.Replace("STRING", "");
                        int.TryParse(typeName.Trim(), out int length);
                        type = typeof(char[]);
                        length += 1;
                        dynamic value = Array.CreateInstance(type.GetElementType(), length);
                        dicProperty.Add(info.Name, value);
                    }
                    else
                    {
                        dynamic value = null;
                        if (typeName.Contains("BOOL"))
                        {
                            value = false;
                        }
                        else if (typeName.Contains("UDINT"))
                        {
                            uint a = 0;
                            value = a;
                        }
                        else if (typeName.Contains("DINT"))
                        {
                            int a = 0;
                            value = a;
                        }
                        else if (typeName.Contains("UINT"))
                        {
                            ushort a = 0;
                            value = a;
                        }
                        else if (typeName.Contains("INT"))
                        {
                            short a = 0;
                            value = a;
                        }
                        else if (typeName.Contains("LREAL"))
                        {
                            value = 0d;
                        }
                        else if (typeName.Contains("REAL"))
                        {
                            value = 0f;
                        }
                        if (value != null)
                        {
                            strType = strType.Replace(";", "");
                            strType = strType.Replace(":", "");
                            strType = strType.Trim();
                            //name;
                            if (dicInfoList.ContainsKey(strType))
                            {

                            }
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

                //生成动态类型
                Type? type1 = dicProperty.GetDynamicType(AssemblyName, strType, dicInfoList);
                if (type1 != null)
                {
                    dynamic dyn = Activator.CreateInstance(type1);

                    if (dyn != null)
                    {
                        Type type2 = dyn.GetType();
                        foreach (var item in dicProperty)
                        {
                            //动态类型里面是属性赋初始值
                            PropertyInfo? propertyInfo = type2.GetProperty(item.Key);
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
}
