using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace DRSoft.Runtime.MVVM.Toolkit
{
    public static class Message
    {
        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached(
                "Attach",
                typeof(string),
                typeof(Message),
                new PropertyMetadata(null, OnAttachChanged)
            );

        public static string GetAttach(DependencyObject obj) => (string)obj.GetValue(AttachProperty);
        public static void SetAttach(DependencyObject obj, string value) => obj.SetValue(AttachProperty, value);


        private static void OnAttachChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                var message = (string)e.NewValue;
                if (message.EndsWith(';'))
                {
                    message = message.Substring(0, message.Length - 1);
                }
                var parsedList = MessageParser.ParseMessage(message); // 解析出事件名和方法名
                foreach (var parsed in parsedList)
                {
                    EventInfo eventInfo = element.GetType().GetEvent(parsed.EventName);
                    string uid = element.Uid;
                    FrameworkElement element1 = element as FrameworkElement;

                    
                    if (element1.DataContext == null)
                    {
                        // 延迟到 DataContext 就绪后执行
                        element1.DataContextChanged += (s, _) => BindEvent((string)e.NewValue, element1, eventInfo, parsed);
                    }
                    else
                    {
                        BindEvent((string)e.NewValue, element1, eventInfo, parsed);
                    }
                }
            }
        }


        internal static readonly DependencyProperty Tag1Property =
        DependencyProperty.RegisterAttached("Tag1", typeof(string), typeof(Message), new PropertyMetadata(string.Empty));

        public static void SetTag1(DependencyObject element, string value)
        {
            element.SetValue(Tag1Property, value);
        }

        public static string GetTag1(DependencyObject element)
        {
            return (string)element.GetValue(Tag1Property);
        }

        // 绑定事件到 ViewModel 方法
        private static void BindEvent(string message, FrameworkElement element, EventInfo eventInfo, ParsedMessage parsed)
        {
            try
            {
                Message.SetTag1(element, message);
                string methodName = parsed.MethodName;
                ViewAware viewModel = element.DataContext as ViewAware;
                if (viewModel == null) return;
                IEnumerable<MethodInfo> methodInfos = viewModel.GetType().GetMethods()?.Where(p => p.Name == parsed.MethodName && p.IsPublic);
                if (methodInfos != null && methodInfos.Count() > 1)
                {
                    MethodInfo? method = methodInfos.FirstOrDefault(p => p.GetParameters().Length == parsed.Parameters.Count);
                    if (method == null)
                    {
                        method = methodInfos.OrderBy(p => p.GetParameters().Length).FirstOrDefault();
                    }
                    if (method != null)
                    {
                        object[] parameterArray = GetInvockParameter(method, parsed.Parameters);
                        if (!viewModel.ParameterArrayDic.ContainsKey(message))
                        {
                            viewModel.ParameterArrayDic.TryAdd(message, (method, parameterArray));
                        }

                        // 创建通用的事件处理器委托，适配不同参数类型
                        Type eventInfoType = eventInfo.EventHandlerType;
                        // 创建动态方法

                        Action<object, object> action = (send, age) =>
                        {
                            FrameworkElement framework = send as FrameworkElement;
                            ViewAware viewModel = framework.DataContext as ViewAware;
                            string tag = Message.GetTag1(framework);
                            (MethodInfo, object[]) methodInfoParams = viewModel.ParameterArrayDic[tag];
                            MethodInfo method = methodInfoParams.Item1;
                            object[] parameterArray = methodInfoParams.Item2;
                            if (parameterArray != null && parameterArray.Any())
                            {
                                if (parameterArray[0] != null && parameterArray[0].ToString() == "$source")
                                {
                                    parameterArray[0] = send;
                                }
                                if (parameterArray.Length > 1 && parameterArray[1] != null && parameterArray[1].ToString() == "$eventargs")
                                {
                                    parameterArray[1] = age;
                                }

                                if (parameterArray[0] != null && parameterArray[0].ToString() == "$eventargs")
                                {
                                    parameterArray[0] = age;
                                }
                                if (parameterArray.Length > 1 && parameterArray[1] != null && parameterArray[1].ToString() == "$source")
                                {
                                    parameterArray[1] = send;
                                }
                            }
                            try
                            {
                                method.Invoke(viewModel, parameterArray);
                            }
                            catch (Exception)
                            {
                            }
                        };
                        MethodInfo dynamicMethodInfo = CreateDynamicMethod(eventInfoType, method, action);
                        Delegate handler = dynamicMethodInfo.CreateDelegate(eventInfoType);
                        eventInfo.AddEventHandler(element, handler);
                    }
                }
            }
            catch (Exception)
            {

            }
        }


        private static MethodInfo CreateDynamicMethod(Type eventInfoType, MethodInfo targetMethod, Action<object, object> callback)
        {
            // 解析委托签名
            MethodInfo invokeMethod = eventInfoType.GetMethod("Invoke");
            Type[] delegateParams = Array.ConvertAll(invokeMethod.GetParameters(), p => p.ParameterType);

            // 获取目标方法的信息
            // 创建动态方法，参数类型为targetType，返回类型与目标方法一致
            var dynamicMethod = new DynamicMethod(
                name: $"Dynamic{DateTime.Now.Ticks}",
                returnType: targetMethod.ReturnType,
                parameterTypes: delegateParams,
                owner: targetMethod.DeclaringType, // 可指定模块或类型以提高兼容性
                skipVisibility: true);

            // 生成IL代码
            ILGenerator il = dynamicMethod.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0); // 加载第一个参数（目标实例）

            // 加载所有参数到堆栈
            for (int i = 0; i < 2; i++)
            {
                il.Emit(OpCodes.Ldarg, i);
            }
            if (targetMethod.IsVirtual)
                il.Emit(OpCodes.Callvirt, callback.Method); // 调用虚方法
            else
                il.Emit(OpCodes.Call, callback.Method); // 调用非虚方法
            il.Emit(OpCodes.Ret); // 返回

            return dynamicMethod;
        }

        private static object[] GetInvockParameter(MethodInfo method, List<string> parameters)
        {
            ParameterInfo[] parameterInfos = method.GetParameters();
            List<object> methodParams = null;
            if (parameters != null && parameterInfos.Length == parameters.Count)
            {
                if (parameters.Any())
                {
                    methodParams ??= new List<object>();
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        if (parameters[i].Trim().ToLower() == "$source")
                        {
                            methodParams.Add(parameters[i].Trim().ToLower());
                            continue;
                        }
                        if (parameters[i].Trim().ToLower() == "$eventargs")
                        {
                            methodParams.Add(parameters[i].Trim().ToLower());
                            continue;
                        }
                        if (parameters[i][0] == '\'' && parameters[i][parameters[i].Length - 1] == '\'')
                        {
                            string param = parameters[i].Substring(1, parameters[i].Length - 2);
                            object obj = Convert(param, parameterInfos[i].ParameterType);
                            methodParams.Add(obj);
                            continue;
                        }
                        else
                        {
                            object obj = Convert(parameters[i], parameterInfos[i].ParameterType);
                            methodParams.Add(obj);
                            continue;
                        }
                    }
                }
            }
            return methodParams?.ToArray();
        }


        public static object Convert(string value, Type type)
        {
            try
            {
                if (type == typeof(bool))
                {
                    if (value == "1")
                    {
                        return true;
                    }
                    else if (value == "0")
                    {
                        return false;
                    }
                    else if (value.ToLower() == "false")
                    {
                        value = "False";
                        return bool.Parse(value);
                    }
                    else if (value.ToLower() == "true")
                    {
                        value = "True";
                        return bool.Parse(value);
                    }
                    return false;
                }
                else if (type == typeof(char))
                {
                    return char.Parse(value);
                }
                else if (type == typeof(short))
                {
                    return short.Parse(value);
                }
                else if (type == typeof(int))
                {
                    return int.Parse(value);
                }
                else if (type == typeof(float))
                {
                    return float.Parse(value);
                }
                else if (type == typeof(double))
                {
                    return double.Parse(value);
                }
                else if (type == typeof(long))
                {
                    return long.Parse(value);
                }
                else if (type == typeof(ushort))
                {
                    return ushort.Parse(value);
                }
                else if (type == typeof(uint))
                {
                    return uint.Parse(value);
                }
                else if (type == typeof(ulong))
                {
                    return ulong.Parse(value);
                }
                else if (type == typeof(string))
                {
                    return value;
                }
                // 添加其他类型支持（如 double、decimal 等）
                else
                {
                    return Activator.CreateInstance(type);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 动态绑定事件处理方法
        /// </summary>
        /// <param name="target">事件所属对象（如 Button 实例）</param>
        /// <param name="eventName">事件名称（如 "Click"）</param>
        /// <param name="handlerMethod">处理方法的方法信息</param>
        public static void SubscribeEvent(object target, string eventName, MethodInfo handlerMethod)
        {
            EventInfo eventInfo = target.GetType().GetEvent(eventName);
            if (eventInfo == null)
                throw new ArgumentException($"事件 {eventName} 不存在");

            // 创建委托
            Delegate handler;
            if (handlerMethod.IsStatic)
                handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, handlerMethod);
            else
                handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, handlerMethod.DeclaringType, handlerMethod.Name);

            
            // 订阅事件
            eventInfo.AddEventHandler(target, handler);
        }

        private static Delegate CreateEventHandler(dynamic element, string methodName, List<string> parameters)
        {
            return new EventHandler((sender, args) => {
                IViewAware viewModel = element.DataContext; // 获取DataContext（即ViewModel）
                MethodInfo method = viewModel.GetType().GetMethod(methodName);
                ParameterInfo[]  parameterInfos = method.GetParameters();
                List<object> methodParams = null;
                if (parameterInfos.Length == parameters.Count)
                {
                    if (parameters.Any())
                    {
                        methodParams ??= new List<object>();
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            if (parameters[i] == "$source")
                            {
                                if (parameterInfos[i].ParameterType == sender.GetType())
                                {
                                    methodParams[i] = sender;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else if (parameters[i] == "$Source")
                            {
                                if (parameterInfos[i].ParameterType == sender.GetType())
                                {
                                    methodParams[i] = sender;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            if (parameters[i] == "$eventArgs")
                            {
                                if (parameterInfos[i].ParameterType is EventArgs)
                                {
                                    methodParams[i] = args;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else if (parameters[i] == "$EventArgs")
                            {
                                if (parameterInfos[i].ParameterType is EventArgs)
                                {
                                    methodParams[i] = args;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else if (parameters[i][0] == '\'' && parameters[i][parameters[i].Length - 1] == '\'')
                            {
                                string param = parameters[i].Substring(1, parameters[i].Length - 1);
                                object obj = Convert(param, parameterInfos[i].ParameterType);
                                methodParams[i] = obj;
                            }
                            else
                            {
                                object obj = Convert(parameters[i], parameterInfos[i].ParameterType);
                                methodParams[i] = obj;
                            }
                        }
                    }
                }
                method.Invoke(viewModel, methodParams?.ToArray());
            });
        }

        private static object[] ResolveParameters(List<string> parameters, object sender, EventArgs eventArgs)
        {
            return parameters.Select(p => {
                if (p == "$source") return sender;
                if (p == "$eventArgs") return eventArgs;
                return parameters;
            }).ToArray();
        }
    }
}
