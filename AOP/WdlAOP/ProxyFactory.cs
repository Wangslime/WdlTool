using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WdlProxyAOP
{
    public class MyProxy<T> : DispatchProxy
    {
        public T _Instance = default;

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            return Process(_Instance, targetMethod, args);
        }

        private object Process(T instance, MethodInfo targetMethod, params object[] args)
        {
            object execResult = null;

            List<FilterAttribute> filters = null;
            if (instance != null && instance.GetType() != null && instance.GetType().GetMethod(targetMethod.Name) != null)
            {
                filters = instance.GetType().GetMethod(targetMethod.Name).GetCustomAttributes<FilterAttribute>(true).ToList();

                List<FilterAttribute> clssFilters = instance.GetType().GetCustomAttributes<FilterAttribute>(true).ToList();
                foreach (FilterAttribute filterAttribute in clssFilters)
                {
                    if (!filters.Any(p=> p.TypeId == filterAttribute.TypeId))
                    {
                        if (filters != null)
                        {
                            filters.Add(filterAttribute);
                        }
                    }
                }
            }
            List<FilterAttribute> execBeforeFilters = new List<FilterAttribute>();
            List<FilterAttribute> execAfterFilters = new List<FilterAttribute>();
            List<FilterAttribute> exceptionFilters = new List<FilterAttribute>();
            if (filters != null && filters.Count() > 0)
            {
                execBeforeFilters = filters.Where(f => f.FilterType == FilterType.BEFORE).ToList();
                execAfterFilters = filters.Where(f => f.FilterType == FilterType.AFTER).ToList();
                exceptionFilters = filters.Where(f => f.FilterType == FilterType.EXCEPTION).ToList();
            }
            try
            {
                if (execBeforeFilters != null && execBeforeFilters.Count > 0)
                {
                    execBeforeFilters.ForEach(f => f.Execute(instance, targetMethod, null));
                }
                var mParams = targetMethod.GetParameters();
                object[] newArgs = new object[args.Length];
                for (int i = 0; i < mParams.Length; i++)
                {
                    newArgs[i] = Convert.ChangeType(args[i], mParams[i].ParameterType);
                }
                execResult = targetMethod.Invoke(instance, newArgs);
                if (execBeforeFilters != null && execBeforeFilters.Count > 0)
                {
                    execAfterFilters.ForEach(f => f.Execute(instance, targetMethod, execResult));
                }
            }
            catch (Exception ex)
            {
                if (exceptionFilters != null && exceptionFilters.Count > 0)
                {
                    exceptionFilters.ForEach(f => f.Execute(instance, targetMethod, ex));
                }
            }
            return execResult;
        }
    }

    public static class ProxyFactory
    {
        public static T Creat<T>(T t)
        {
            T tProxy = DispatchProxy.Create<T, MyProxy<T>>();
            FieldInfo fieldInfo = tProxy.GetType().GetField("_Instance");
            fieldInfo.SetValue(tProxy, t);
            return tProxy;
        }
    }
}