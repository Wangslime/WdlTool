using DRSoft.Runtime.MVVM.Toolkit.Interfaces;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace DRSoft.Runtime.MVVM.Toolkit;

public class WindowManager : IWindowManager
{
    public async Task<bool?> ShowDialogAsync(object rootModel, object? context = null, IDictionary<string, object> settings = null)
    {
        var window = await CreateWindowAsync(rootModel, true, context, settings);

        return window.ShowDialog();
    }

    public async Task ShowWindowAsync(object rootModel, object? context = null, IDictionary<string, object>? settings = null)
    {
        var window = await CreateWindowAsync(rootModel, false, context, settings);

        window.Show();
    }

    /// <summary>
    /// Shows a popup at the current mouse position.
    /// </summary>
    /// <param name="rootModel">The root model.</param>
    /// <param name="context">The view context.</param>
    /// <param name="settings">The optional popup settings.</param>
    public async Task ShowPopupAsync(object rootModel, object? context = null, IDictionary<string, object>? settings = null)
    {
        var popup = CreatePopup(rootModel, settings);
        var view = ViewLocator.LocateViewForViewModel(rootModel, popup, context);

        popup.Child = view;

        ((FrameworkElement)view).DataContext = rootModel;

        popup.IsOpen = true;
        popup.CaptureMouse();
    }


    protected Popup CreatePopup(object rootModel, IDictionary<string, object>? settings)
    {
        var popup = new Popup();

        if (ApplySettings(popup, settings))
        {
            if (!settings.ContainsKey("PlacementTarget") && !settings.ContainsKey("Placement"))
            {
                popup.Placement = PlacementMode.MousePoint;
            }

            if (!settings.ContainsKey("AllowsTransparency"))
            {
                popup.AllowsTransparency = true;
            }
        }
        else
        {
            popup.AllowsTransparency = true;
            popup.Placement = PlacementMode.MousePoint;
        }

        return popup;
    }

    protected virtual async Task<Window> CreateWindowAsync(object rootViewModel, bool isDialog, object? context, IDictionary<string, object>? settings)
    {
        var view = EnsureWindow(rootViewModel, ViewLocator.LocateViewForViewModel(rootViewModel, null, context), isDialog);
        view.DataContext = rootViewModel;
        IViewAware viewAware = rootViewModel as IViewAware;
        viewAware.GetView();
        viewAware.AttachView(view, context);
        ApplySettings(view, settings);

        return view;
    }

    protected virtual Window EnsureWindow(object model, object view, bool isDialog)
    {

        if (view is Window window)
        {
            var owner = InferOwnerOf(window);
            if (owner != null && isDialog)
            {
                window.Owner = owner;
            }
        }
        else
        {
            window = new Window
            {
                Content = view,
                SizeToContent = SizeToContent.WidthAndHeight
            };

            var owner = InferOwnerOf(window);
            if (owner != null)
            {
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                window.Owner = owner;
            }
            else
            {
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
        }

        return window;
    }

    protected virtual Window? InferOwnerOf(Window window)
    {
        var application = Application.Current;
        if (application == null)
        {
            return null;
        }

        var active = application.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
        active = active ?? (PresentationSource.FromVisual(application.MainWindow) == null ? null : application.MainWindow);
        return active == window ? null : active;
    }

    private bool ApplySettings(object target, IEnumerable<KeyValuePair<string, object>>? settings)
    {
        if (settings != null)
        {
            var type = target.GetType();

            foreach (var pair in settings)
            {
                var propertyInfo = type.GetProperty(pair.Key);

                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(target, pair.Value, null);
                }
            }

            return true;
        }

        return false;
    }
}