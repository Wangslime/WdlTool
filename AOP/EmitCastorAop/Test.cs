using Castle.DynamicProxy;
using System.Collections;

namespace EmitCastorAop
{
    internal class Test
    {
        public void Test1()
        {
            IList list = new List<int>();
            ProxyGenerator generator = new ProxyGenerator();
            CastomInterceptor interceptor = new CastomInterceptor();
            list = generator.CreateInterfaceProxyWithTarget(list, interceptor);
            list.Add(1);
            list.Add(2);
        }
    }
}
