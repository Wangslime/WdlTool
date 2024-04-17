using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Drsoft.Plugin.PublicExtend
{
    public static class CExpression
    {
        public static dynamic? ExpressionCreateObj(this Type type, params object?[]? args)
        {
            var expression = Expression.New(type);         // 创建一个 New 表达式，表示创建 T 类型的新对象
            var lambda = Expression.Lambda(expression);    // 创建一个 Lambda 表达式，将 New 表达式封装为一个 Func<T> 委托
            return lambda.Compile().DynamicInvoke(args);   // 编译 Lambda 表达式以生成 Func<T> 委托
        }

        public static dynamic? ExpressionCreateObj<T>(params object?[]? args)
        {
            var expression = Expression.New(typeof(T));         // 创建一个 New 表达式，表示创建 T 类型的新对象
            var lambda = Expression.Lambda<Func<T>>(expression);// 创建一个 Lambda 表达式，将 New 表达式封装为一个 Func<T> 委托
            return lambda.Compile().DynamicInvoke(args);        // 编译 Lambda 表达式以生成 Func<T> 委托
        }

        public static dynamic EmitCreateObj(Type type)
        {
            var dynamicMethod = new DynamicMethod("CreateInstance", type, Type.EmptyTypes, type.Module);
            var ilGenerator = dynamicMethod.GetILGenerator();

            // 调用 IL 指令生成对象并将其推送到堆栈上
            ilGenerator.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
            ilGenerator.Emit(OpCodes.Ret);

            // 创建委托并调用
            var createInstanceDelegate = (Func<object>)dynamicMethod.CreateDelegate(typeof(Func<object>));
            return createInstanceDelegate();
        }

        public static dynamic EmitCreateObj<T>()
        {
            Type type = typeof(T);
            var dynamicMethod = new DynamicMethod("CreateInstance", type, Type.EmptyTypes, type.Module);
            var ilGenerator = dynamicMethod.GetILGenerator();

            // 调用 IL 指令生成对象并将其推送到堆栈上
            ilGenerator.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
            ilGenerator.Emit(OpCodes.Ret);

            // 创建委托并调用
            var createInstanceDelegate = (Func<T>)dynamicMethod.CreateDelegate(typeof(Func<T>));
            return createInstanceDelegate();
        }
    }
}