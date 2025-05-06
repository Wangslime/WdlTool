using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;

namespace DRSoft.Runtime.MVVM.Toolkit;

public static class View
{
    private static bool? _isDesignMode;

    public static bool IsDesignMode
    {
        get
        {
            if (!_isDesignMode.HasValue)
            {
                var descriptor = DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement));
                _isDesignMode = (bool)descriptor.Metadata.DefaultValue;
            }

            return _isDesignMode == true;
        }
    }

    private static readonly ContentPropertyAttribute DefaultContentProperty = new("Content");

    #region ViewModelProperty

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.RegisterAttached("ViewModel", typeof(object), typeof(View), new PropertyMetadata(default, OnViewModelChanged));

    public static void SetViewModel(DependencyObject element, object value)
    {
        element.SetValue(ViewModelProperty, value);
    }

    public static object? GetViewModel(DependencyObject element)
    {
        return element.GetValue(ViewModelProperty);
    }

    private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (ReferenceEquals(e.OldValue, e.NewValue))
        {
            return;
        }

        if (e.NewValue != null)
        {
            var context = GetContext(d);

            var view = ViewLocator.LocateViewForViewModel(e.NewValue, d, context);

            ((FrameworkElement)view).DataContext = e.NewValue;
            if (e.NewValue is IViewAware viewAware)
            {
                viewAware.AttachView(view, context);
            }

            if (!SetContentProperty(d, view))
            {
                view = ViewLocator.LocateViewForViewModelType(e.NewValue.GetType(), d, context);

                SetContentProperty(d, view);
            }
        }
        else
        {
            SetContentProperty(d, e.NewValue);
        }
    }

    #endregion

    #region ContextProperty

    public static readonly DependencyProperty ContextProperty =
        DependencyProperty.RegisterAttached("Context", typeof(object), typeof(View),
                                            new PropertyMetadata(default, OnContextChanged));

    public static void SetContext(DependencyObject element, object value)
    {
        element.SetValue(ContextProperty, value);
    }

    public static object? GetContext(DependencyObject element)
    { 
        return element.GetValue(ContextProperty);
    }

    private static void OnContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (ReferenceEquals(e.NewValue, e.OldValue))
        {
            return;
        }

        var viewModel = GetViewModel(d);
        if (viewModel == null)
        {
            return;
        }

        var view = ViewLocator.LocateViewForViewModel(viewModel, d, e.NewValue);

        if (!SetContentProperty(d, view))
        {

            view = ViewLocator.LocateViewForViewModelType(viewModel.GetType(), d, e.NewValue);

            SetContentProperty(d, view);
        }

        ((FrameworkElement)view).DataContext = viewModel;
    }

    #endregion

    private static bool SetContentProperty(object targetLocation, object? view)
    {
        if (view is FrameworkElement { Parent: not null } frameworkElement)
        {
            SetContentPropertyCore(frameworkElement.Parent, null);
        }

        return SetContentPropertyCore(targetLocation, view);
    }

    private static bool SetContentPropertyCore(object targetLocation, object? view)
    {
        try
        {
            
            var type = targetLocation.GetType();
            var contentProperty = type.GetCustomAttributes(typeof(ContentPropertyAttribute), true)
                                      .OfType<ContentPropertyAttribute>()
                                      .FirstOrDefault() ?? DefaultContentProperty;

            type.GetProperty(contentProperty.Name ?? DefaultContentProperty.Name)?.SetValue(targetLocation, view, null);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}