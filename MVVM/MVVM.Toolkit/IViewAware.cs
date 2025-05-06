namespace DRSoft.Runtime.MVVM.Toolkit;

public interface IViewAware
{
    void AttachView(object view, object? context = null);

    object? GetView(object? context = null);

    T? GetView<T>(object? context = null) where T : class;

    event EventHandler<ViewAttachedEventArgs> ViewAttached;
}

public class ViewAttachedEventArgs : EventArgs
{
    public ViewAttachedEventArgs(object view, object? context = null)
    {
        View = view;
        Context = context;
    }

    public object View { get; set; }

    public object? Context { get; set; }
}