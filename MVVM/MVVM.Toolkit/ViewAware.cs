using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Concurrent;
using System.Reflection;
using System.Windows;

namespace DRSoft.Runtime.MVVM.Toolkit;

public class ViewAware : ObservableObject, IViewAware
{
    public ConcurrentDictionary<string, (MethodInfo, object[])> ParameterArrayDic = new ConcurrentDictionary<string, (MethodInfo,object[])>();
    protected Dictionary<object, WeakReference<object>> Views { get; } = new();

    public static readonly object DefaultContext = new();

    protected ViewAware()
    {
    }

    void IViewAware.AttachView(object view, object? context)
    {
        Views[context ?? DefaultContext] = new WeakReference<object>(view);

        // 如果 View 还没有附加到 ViewModel 上，调用 OnViewLoad 方法
        if (view is FrameworkElement element && !(bool)element.GetValue(AttachedProperties.PreviouslyAttachedProperty))
        {
            element.SetValue(AttachedProperties.PreviouslyAttachedProperty, true);
            // 如果 View 已经加载，直接调用 OnViewLoaded 方法
            // 否则，等到 View 加载完成后再利用 FrameworkElement 的 Loaded 事件调用 OnViewLoaded 方法，调用完成后取消事件的订阅
            if (element.IsLoaded)
            {
                OnViewLoaded(view);
            }
            else
            {
                RoutedEventHandler? loaded = null;
                loaded = (s, e) =>
                {
                    element.Loaded -= loaded;
                    OnViewLoaded(view);
                };
                element.Loaded += loaded;
            }
        }

        // 通知 View 已经附加到 ViewModel 上
        OnViewAttached(view, context);
        ViewAttached?.Invoke(this, new ViewAttachedEventArgs(view, context));

        // TODO: 添加 Activate 相关内容
    }

    public object? GetView(object? context = null)
    {
        Views.TryGetValue(context ?? DefaultContext, out WeakReference<object>? value);
        return value?.TryGetTarget(out object? target) == true ? target : null;
    }

    public T? GetView<T>(object? context = null) where T : class
    {
        return GetView(context) as T;
    }

    public event EventHandler<ViewAttachedEventArgs>? ViewAttached;

    /// <summary>
    /// 当 View 附加到 ViewModel 上时调用
    /// </summary>
    /// <param name="view">附加到 ViewModel 上的 View</param>
    /// <param name="context">当一个 ViewModel 绑定到多个 View 时，用该参数区分 View 的上下文</param>
    protected virtual void OnViewAttached(object view, object? context = null)
    {
    }

    /// <summary>
    /// 当 View 加载完成时调用
    /// </summary>
    /// <param name="view">加载完成的 View</param>
    protected virtual void OnViewLoaded(object view)
    {
    }
}