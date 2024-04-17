using System.Diagnostics;
using System.Reflection;

namespace Drsoft.Plugin.ProxyAop
{
    public class MyProxy<T> : DispatchProxy
    {
        public T _Instance = default;

        List<MethodInfo> clssMethod = typeof(T).GetMethods().ToList();
        List<FilterAttribute> clssFilters = typeof(T).GetCustomAttributes<FilterAttribute>(true).ToList();

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            return Process(_Instance, targetMethod, args);
        }

        private object Process(T instance, MethodInfo targetMethod, params object[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (targetMethod.Name.StartsWith("get") || targetMethod.Name.StartsWith("set"))
            {
                return targetMethod.Invoke(instance, args);
            }
            object execResult = null;
            List<FilterAttribute> filters = clssFilters.ToList();
            filters ??= new List<FilterAttribute>();
            IEnumerable<MethodInfo> methods = clssMethod.Where(p => p.Name == targetMethod.Name);
            if (methods != null && methods.Any())
            {
                foreach (var item in methods)
                {
                    IEnumerable<FilterAttribute> MethodInfofilters = item.GetCustomAttributes<FilterAttribute>(true);
                    if (MethodInfofilters != null)
                    {
                        foreach (FilterAttribute filterAttribute in MethodInfofilters)
                        {
                            if (!filters.Any(p => p.TypeId == filterAttribute.TypeId))
                            {
                                filters?.Add(filterAttribute);
                            }
                        }
                    }
                }
            }
            IEnumerable<FilterAttribute> execBeforeFilters = null;
            IEnumerable<FilterAttribute> execAfterFilters = null;
            IEnumerable<FilterAttribute> exceptionFilters = null;
            if (filters != null && filters.Count() > 0)
            {
                execBeforeFilters = filters.Where(f => f.FilterType == FilterType.BEFORE);
                execAfterFilters = filters.Where(f => f.FilterType == FilterType.AFTER);
                exceptionFilters = filters.Where(f => f.FilterType == FilterType.EXCEPTION);
            }
            try
            {
                if (execBeforeFilters != null)
                {
                    foreach (var item in execBeforeFilters)
                    {
                        item.Execute<T>(instance, targetMethod, null, stopwatch.ElapsedMilliseconds, args);
                    }
                }
                execResult = targetMethod.Invoke(instance, args);
                if (execAfterFilters != null)
                {
                    foreach (var item in execAfterFilters)
                    {
                        item.Execute<T>(instance, targetMethod, execResult, stopwatch.ElapsedMilliseconds, args);
                    }
                }
            }
            catch (Exception ex)
            {
                if (exceptionFilters != null)
                {
                    foreach (var item in exceptionFilters)
                    {
                        item.Execute<T>(instance, targetMethod, ex, stopwatch.ElapsedMilliseconds, args);
                    }
                }
            }
            finally
            {
                stopwatch.Stop();
            }
            return execResult;
        }
    }

    public static class ProxyFactory
    {
        public static T Creat<T>(T t)
        {
            dynamic tProxy = DispatchProxy.Create<T, MyProxy<T>>();
            tProxy._Instance = t;
            return tProxy;
        }
    }
}