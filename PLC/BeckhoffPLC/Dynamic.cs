using System.Reflection;
using System.Reflection.Emit;

namespace BeckhoffPLC
{
    public static class Dynamic
    {
        private static int count = 0;
        public static ModuleBuilder moduleBuilder { get; set; } = null;
        public static AssemblyBuilder assemblyBuilder { get; set; } = null;

        /// <summary>
        /// 定义一个动态程序集
        /// </summary>
        /// <returns></returns>
        private static ModuleBuilder GetDynamicModule(string AssemblyName = "MyAssembly", string Module = "DynamicModule")
        {
            if (moduleBuilder != null) 
            {
                return moduleBuilder;
            }
            AssemblyName assemblyName = new AssemblyName(AssemblyName);
            if (assemblyBuilder == null)
            {
                assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
            }
            ModuleBuilder module = assemblyBuilder.DefineDynamicModule(Module);
            moduleBuilder = module;
            return moduleBuilder;
        }

        public static Type? GetDynamicType(this Dictionary<string,dynamic> dicProperty, string typeName = "")
        {
            if (string.IsNullOrEmpty(typeName))
            {
                count++;
                typeName = $"Dynamic{count}";
            }
            // 创建动态类型
            ModuleBuilder moduleBuilder = GetDynamicModule();
            if (moduleBuilder.GetTypes().Any(p=> p.Name == typeName))
            {
                return moduleBuilder.GetType(typeName);
            }
            else
            {
                TypeBuilder typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public);

                //创建无参构造函数
                ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
                ILGenerator constructorIL = constructorBuilder.GetILGenerator();
                constructorIL.Emit(OpCodes.Ret);

                if (dicProperty != null && dicProperty.Any())
                {
                    foreach (var item in dicProperty)
                    {
                        Type type = null;
                        if (item.Value is Type)
                        {
                            type = item.Value;
                        }
                        else
                        {
                            type = item.Value.GetType();
                        }
                        typeBuilder.SetProperty(item.Key, type);
                    }
                }
                return typeBuilder?.CreateType();
            }
        }

        public static Type? GetDynamicType(string name, Type type)
        {
            // 创建动态类型
            string typeName = name + "Class";
            ModuleBuilder moduleBuilder = GetDynamicModule();
            {
                TypeBuilder typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public);

                //创建无参构造函数
                ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
                ILGenerator constructorIL = constructorBuilder.GetILGenerator();
                constructorIL.Emit(OpCodes.Ret);
                typeBuilder.SetProperty(name, type);
                return typeBuilder?.CreateType();
            }
        }


        /// <summary>
        /// 动态添加属性
        /// </summary>
        /// <param name="typeBuilder"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyType"></param>
        private static void SetProperty(this TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            // 定义一个动态属性
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);

            // 定义一个动态字段
            FieldBuilder fieldBuilder = typeBuilder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            // 定义属性的Get和Set方法
            MethodBuilder getMethodBuilder =
                typeBuilder.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            ILGenerator getILGenerator = getMethodBuilder.GetILGenerator();
            getILGenerator.Emit(OpCodes.Ldarg_0);
            getILGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
            getILGenerator.Emit(OpCodes.Ret);

            MethodBuilder setMethodBuilder =
                typeBuilder.DefineMethod("set_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new Type[] { propertyType });
            ILGenerator setILGenerator = setMethodBuilder.GetILGenerator();
            setILGenerator.Emit(OpCodes.Ldarg_0);
            setILGenerator.Emit(OpCodes.Ldarg_1);
            setILGenerator.Emit(OpCodes.Stfld, fieldBuilder);
            setILGenerator.Emit(OpCodes.Ret);

            // 关联属性的Get和Set方法
            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);
        }
    }
}
