using System;

namespace WdlNetIoc
{
    public static class ExtendIocProvider
    {
        #region AddScopedClass
        public static void AddScoped<T>(this IocProvider iocProvider)
        {
            iocProvider.AddScoped(typeof(T));
        }
        public static void AddScoped<T>(this IocProvider iocProvider, T t)
        {
            iocProvider.AddScoped(t.GetType());
        }
        public static void AddScoped<T>(this IocProvider iocProvider, Func<T> func)
        {
            iocProvider.AddScoped(func.Invoke().GetType());
        }
        public static void AddScoped(this IocProvider iocProvider, Type type)
        {
            iocProvider.AddScoped(type);
        }
        #endregion

        #region AddScopedInterface
        public static void AddScoped<ET, IT>(this IocProvider iocProvider)
        {
            iocProvider.AddScoped(typeof(ET), typeof(IT));
        }
        public static void AddScoped<ET, IT>(this IocProvider iocProvider, ET et, IT it)
        {
            iocProvider.AddScoped(et.GetType(), it.GetType());
        }
        public static void AddScoped<ET, IT>(this IocProvider iocProvider, Func<IT> func)
        {
            iocProvider.AddScoped(typeof(ET), func.Invoke().GetType());
        }
        public static void AddScoped(this IocProvider iocProvider, Type EType, Type IType)
        {
            iocProvider.AddScoped(EType, IType);
        }
        #endregion

        #region AddSingletonClass
        public static void AddSingleton<T>(this IocProvider iocProvider)
        {
            iocProvider.AddSingleton(typeof(T));
        }
        public static void AddSingleton<T>(this IocProvider iocProvider, T t)
        {
            iocProvider.AddSingleton(t.GetType());
        }
        public static void AddSingleton<T>(this IocProvider iocProvider, Func<T> func)
        {
            iocProvider.AddSingleton(func.Invoke().GetType());
        }
        public static void AddSingleton(this IocProvider iocProvider, Type type)
        {
            iocProvider.AddSingleton(type);
        }
        #endregion

        #region AddSingletonInterface
        public static void AddSingleton<ET, IT>(this IocProvider iocProvider)
        {
            iocProvider.AddSingleton(typeof(ET), typeof(IT));
        }
        public static void AddSingleton<ET, IT>(this IocProvider iocProvider, ET et, IT it)
        {
            iocProvider.AddSingleton(et.GetType(), it.GetType());
        }
        public static void AddSingleton<ET, IT>(this IocProvider iocProvider, Func<IT> func)
        {
            iocProvider.AddSingleton(typeof(ET), func.Invoke().GetType());
        }
        public static void AddSingleton(this IocProvider iocProvider, Type EType, Type IType)
        {
            iocProvider.AddSingleton(EType, IType);
        }
        #endregion

        #region GetService
        public static object GetService(this IocProvider iocProvider, Type type)
        {
            return iocProvider.IocGetService(type);
        }
        public static T GetService<T>(this IocProvider iocProvider) where T : class
        {
            return (T)iocProvider.IocGetService(typeof(T));
        }
        #endregion
    }
}
