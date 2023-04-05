using System;

namespace WdlNetIoc
{
    internal class ServiceDescriptor
    {
        internal ServiceLifetime Lifetime { get; set; }

        internal Type TypeKey { get; set; }
        internal Type TypeValue { get; set; }

        internal object Instance { get; set; }

        internal ServiceDescriptor(Type TypeKey, Type TypeValue, ServiceLifetime Lifetime)
        { 
            this.TypeKey = TypeKey;
            this.TypeValue = TypeValue;
            this.Lifetime = Lifetime;
        }

        internal object GetService(object[] paramsList)
        {
            switch (Lifetime)
            {
                case ServiceLifetime.Transient:
                    return Activator.CreateInstance(TypeValue, paramsList);
                case ServiceLifetime.Scoped:
                    return Activator.CreateInstance(TypeValue, paramsList);
                case ServiceLifetime.Singleton:
                    if (Instance == null)
                        Instance = Activator.CreateInstance(TypeValue, paramsList);
                    return Instance;
                default:
                    return Activator.CreateInstance(TypeValue, paramsList);
            }
        }
    }

    internal enum ServiceLifetime
    {
        Singleton = 0,
        Scoped = 1,
        Transient = 2
    }
}
