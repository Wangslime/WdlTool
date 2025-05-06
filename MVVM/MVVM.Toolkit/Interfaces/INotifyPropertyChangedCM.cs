using System.Linq.Expressions;

namespace DRSoft.Runtime.MVVM.Toolkit.Interfaces;

/// <summary>
/// Caliburn Micro 框架的 INotifyPropertyChanged 接口
/// </summary>
public interface INotifyPropertyChangedCM
{
    public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property);

    public void NotifyOfPropertyChange([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null);
}