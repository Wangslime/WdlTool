﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using MiniExcelLibs;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;

namespace BeckhoffPLC
{
    public class BeckhoffPlcData
    {
        public string filePath = @"C:\Users\Lenovo\Documents\上下位参数 (自动保存的).xlsx";
        public TypeInfoName? typeInfoName = null;

        Dictionary<string, List<ExcelInfo>> dicInfoList = new Dictionary<string, List<ExcelInfo>>();
        Dictionary<string, dynamic> dicPropertyValue = new Dictionary<string, dynamic>();
        public async Task<bool> Initial()
        {
            List<string> list = MiniExcel.GetSheetNames(filePath);

            dicInfoList.Clear();
            foreach (string s in list)
            {
                IEnumerable<ExcelInfo> info = await MiniExcel.QueryAsync<ExcelInfo>(filePath, s);

                if (!dicInfoList.ContainsKey(s))
                {
                    dicInfoList.TryAdd(s, info.Where(p => !string.IsNullOrEmpty(p.Name)).ToList());
                }
            }
            var glovalList = dicInfoList["Global_Variables"];

            foreach (var item in glovalList)
            {
                string typeName = item.Type;
                typeName = typeName.Replace(";", "");
                typeName = typeName.Replace(":", "");
                typeName = typeName.Trim();
                if (!list.Contains(typeName))
                {
                    dicInfoList.Add(item.Name + "Class", new List<ExcelInfo>() { new ExcelInfo() { Name = item.Name, Type = item.Type, Description = item.Description } });
                    item.Type = item.Name + "Class";

                    dicInfoList.Remove(item.Name);
                }
            }
            if (glovalList != null && glovalList.Any())
            {
                //根据Excel生成动态程序集，包括各种类和属性对象
                typeInfoName = GetTypeByTypeName("Global_Variables", "Global_Variables", dicInfoList["Global_Variables"]);
                dicPropertyValue.Add("Global_Variables", typeInfoName.Value);
            }


            string code = GenerateCodeFromAssembly(Dynamic.assemblyBuilder);

            //C# 字符串代码生成程序集实体程序集
            var compilation = CSharpCompilation.Create(Dynamic.assemblyBuilder.GetName().Name)
                            .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                            .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                            .AddSyntaxTrees(CSharpSyntaxTree.ParseText(code));

            string baseDirectory = AppContext.BaseDirectory + "MyAssembly.dll";
            if (File.Exists(baseDirectory))
            {
                File.Delete(baseDirectory);
            }
            var result = compilation.Emit("MyAssembly.dll");

            return true;
        }



        /// <summary>
        /// 将动态程序集转换为C#字符串代码
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public string GenerateCodeFromAssembly(AssemblyBuilder assembly)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append('\n');
            sb.Append("using System.Collections.Generic;");
            sb.Append('\n');
            sb.Append("using System.Text;");
            sb.Append('\n');
            sb.Append("using System.Threading.Tasks;");
            sb.Append('\n');
            sb.Append("using System.Runtime.InteropServices;");
            sb.Append('\n');
            sb.Append($"namespace {assembly.GetName().Name}");
            sb.Append('\n');
            sb.Append('{');
            sb.Append('\n');
            #region 命名控件内
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
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
                    string? description = dicInfoList[type.Name].FirstOrDefault(p=> p.Name == property.Name)?.Description;
                    sb.Append("/// <summary>");
                    sb.Append('\n');
                    sb.Append("/// ");
                    sb.Append(description);
                    sb.Append('\n');
                    sb.Append("/// </summary>");
                    dynamic obj1 = property.GetValue(obj);
                    string Unmanaged = GetUnmanagedType(obj1);
                    if (!string.IsNullOrEmpty(Unmanaged))
                    {
                        sb.Append('\n');
                        sb.Append(Unmanaged);
                    }
                    sb.Append($"public {property.PropertyType.FullName} {property.Name}");
                    sb.Append("{get; set;}");
                    sb.Append($"={GetDefaultValue(property.PropertyType, obj1)};");
                    sb.Append('\n');
                }
                #endregion
                sb.Append('}');
                sb.Append('\n');
            }
            #endregion
            sb.Append('}');

            return sb.ToString();
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
                else if (type == typeof(short[]))
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
                else if (type == typeof(short))
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
        /// 通过Type调用泛型方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public async Task<dynamic> QueryExcelByType(Type type, string filePath, string sheetName)
        {
            // 通过反射获取泛型定义并使用 MakeGenericType 方法创建具体泛型类型
            Type genericType = typeof(MyGenericClass<>).MakeGenericType(type);
            // 实例化泛型类
            dynamic? instance = Activator.CreateInstance(genericType);
            // 调用泛型方法
            //MethodInfo methodInfo = genericType.GetMethod("MyMethod");
            //dynamic rest = methodInfo.Invoke(instance, new object[] { filePath, sheetName });
            dynamic task = instance?.MyMethod(filePath, sheetName);
            if (task is Task) return null;
            dynamic result = await task;
            return result;
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
        public TypeInfoName? GetTypeByTypeName(string name, string strType, List<ExcelInfo> excelInfos)
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
                    }
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
                            // 给数组类型赋初始值
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
                Type? type1 = dicProperty.GetDynamicType(strType);
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


    public class ExcelInfo
    {
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public string Description { get; set; } = "";
    }

    public class TypeInfoName
    {
        AlarmClass Alarm = new AlarmClass();

        public string Name { get; set; } = "";
        public dynamic Value { get; set; }
    }

    public class MyGenericClass<T> where T : class, new()
    {
        public Task<IEnumerable<T>> MyMethod(string filePath, string sheetName) 
        {
            return MiniExcel.QueryAsync<T>(filePath, sheetName);
        }
    }
}
