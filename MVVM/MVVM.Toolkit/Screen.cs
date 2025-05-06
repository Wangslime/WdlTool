using DRSoft.Runtime.MVVM.Toolkit.Extensions;
using DRSoft.Runtime.MVVM.Toolkit.Interfaces;
using System.Linq.Expressions;
using System.Windows;

namespace DRSoft.Runtime.MVVM.Toolkit;

public class Screen : ViewAware, INotifyPropertyChangedCM
{
    public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
    {
        OnPropertyChanged(property.GetMemberInfo().Name);
    }

    public void NotifyOfPropertyChange([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        OnPropertyChanged(propertyName);
    }
}