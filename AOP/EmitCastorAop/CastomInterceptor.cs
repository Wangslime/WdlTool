using Castle.DynamicProxy;

namespace EmitCastorAop
{
    /*
         Castor.Core是AutoFac的AOP实现，是最通用的AOP实现
         如果想做到动态扩展功能---可以给接口标记特性，然后Interceptor去反射找到特性，添加相应的功能

      */

    /// <summary>
    /// 拦截器
    /// </summary>
    public class CastomInterceptor : StandardInterceptor
    {
        /// <summary>
        /// 调用前
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation)
        {
            base.PreProceed(invocation);
        }


        /// <summary>
        /// 调用中
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation)
        {
            base.PerformProceed(invocation);
        }


        /// <summary>
        /// 调用后
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            base.PostProceed(invocation);
        }


    }
}