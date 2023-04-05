using System;

namespace WdlNetIoc
{
    public static class ExtendIocContainer
    {
        #region AddScopedClass
        public static void AddScoped<T>(this IocContainer iocContainer)
        {
            iocContainer.AddScoped(typeof(T));
        }
        public static void AddScoped<T>(this IocContainer iocContainer, T t)
        {
            iocContainer.AddScoped(t.GetType());
        }
        public static void AddScoped<T>(this IocContainer iocContainer, Func<T> func)
        {
            iocContainer.AddScoped(func.Invoke().GetType());
        }
        public static void AddScoped(this IocContainer iocContainer, Type type)
        {
            iocContainer.AddScoped(type);
        }
        #endregion

        #region AddScopedInterface
        public static void AddScoped<ET, IT>(this IocContainer iocContainer)
        {
            iocContainer.AddScoped(typeof(ET), typeof(IT));
        }
        public static void AddScoped<ET, IT>(this IocContainer iocContainer, ET et, IT it)
        {
            iocContainer.AddScoped(et.GetType(), it.GetType());
        }
        public static void AddScoped<ET, IT>(this IocContainer iocContainer, Func<IT> func)
        {
            iocContainer.AddScoped(typeof(ET), func.Invoke().GetType());
        }
        public static void AddScoped(this IocContainer iocContainer, Type EType, Type IType)
        {
            iocContainer.AddScoped(EType, IType);
        }
        #endregion

        #region AddSingletonClass
        public static void AddSingleton<T>(this IocContainer iocContainer)
        {
            iocContainer.AddSingleton(typeof(T));
        }
        public static void AddSingleton<T>(this IocContainer iocContainer, T t)
        {
            iocContainer.AddSingleton(t.GetType());
        }
        public static void AddSingleton<T>(this IocContainer iocContainer, Func<T> func)
        {
            iocContainer.AddSingleton(func.Invoke().GetType());
        }
        public static void AddSingleton(this IocContainer iocContainer, Type type)
        {
            iocContainer.AddSingleton(type);
        }
        #endregion

        #region AddSingletonInterface
        public static void AddSingleton<ET, IT>(this IocContainer iocContainer)
        {
            iocContainer.AddSingleton(typeof(ET), typeof(IT));
        }
        public static void AddSingleton<ET, IT>(this IocContainer iocContainer, ET et, IT it)
        {
            iocContainer.AddSingleton(et.GetType(), it.GetType());
        }
        public static void AddSingleton<ET, IT>(this IocContainer iocContainer, Func<IT> func)
        {
            iocContainer.AddSingleton(typeof(ET), func.Invoke().GetType());
        }
        public static void AddSingleton(this IocContainer iocContainer, Type EType, Type IType)
        {
            iocContainer.AddSingleton(EType, IType);
        }
        #endregion

        #region GetService
        public static object GetService(this IocContainer iocContainer, Type type)
        {
            return iocContainer.IocGetService(type);
        }
        public static T GetService<T>(this IocContainer iocContainer) where T : class
        {
            return (T)iocContainer.IocGetService(typeof(T));
        }
        #endregion
    }
}
