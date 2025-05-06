using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace DRSoft.Runtime.MVVM.Toolkit;

public static class ViewLocator
{
    public static UIElement LocateViewForViewModel(object viewModel, DependencyObject? location, object? context)
    {
        if (viewModel is ViewAware viewAware)
        {
            if (viewAware.GetView(context) is UIElement view)
            {
                if (view is not Window window ||
                    (!window.IsLoaded && new WindowInteropHelper(window).Handle != IntPtr.Zero))
                {
                    return view;
                }
            }
        }

        return LocateViewForViewModelType(viewModel.GetType(), location, context);
    }

    public static UIElement LocateViewForViewModelType(Type viewModelType, DependencyObject? location, object? context)
    {
        var viewType = LocateViewTypeForModelType(viewModelType, location, context);

        return viewType is null
                   ? new TextBlock() { Text = $"Can't find view for {viewModelType}" }
                   : GetOrCreateViewType(viewType);
    }

    private static Type? LocateViewTypeForModelType(Type viewModelType, DependencyObject? location, object? context)
    {
        var viewTypeName = viewModelType.FullName!;

        if (viewTypeName.StartsWith('_'))
        {
            var index = viewTypeName.IndexOf('.');
            viewTypeName = viewTypeName[(index + 1)..];
            index = viewTypeName.IndexOf('.');
            viewTypeName = viewTypeName[(index + 1)..];
        }

        viewTypeName = viewTypeName[..(viewTypeName.Contains('`') 
                                           ? viewTypeName.IndexOf('`') 
                                           : viewTypeName.Length)];


        viewTypeName = viewTypeName.Replace("ViewModel", "View");

        if (context != null)
        {
            viewTypeName = viewTypeName.Remove(viewTypeName.Length - 4, 4);
            viewTypeName += "." + context;
        }

        // TODO: [优化] 添加缓存
        var assembly = viewModelType.Assembly;
        var viewType = assembly.GetType(viewTypeName);
        return viewType;
    }

    private static UIElement GetOrCreateViewType(Type viewType)
    {
        if (viewType.IsInterface || viewType.IsAbstract || !typeof(UIElement).IsAssignableFrom(viewType))
        {
            return new TextBlock() { Text = $"Can't create instance of {viewType}" };
        }

        if (Activator.CreateInstance(viewType) is not UIElement view)
        {
            return new TextBlock() { Text = $"Can't create instance of {viewType}" };
        }

        var method = view.GetType().GetMethod("InitializeComponent", BindingFlags.Public | BindingFlags.Instance);
        method?.Invoke(view, null);

        return view;
    }
}