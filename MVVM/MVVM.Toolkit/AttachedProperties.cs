using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DRSoft.Runtime.MVVM.Toolkit
{
    public static class AttachedProperties
    {
        #region PreviouslyAttachedProperty: 标识一个 View 是否已经附加到 ViewModel 上

        public static readonly DependencyProperty PreviouslyAttachedProperty =
            DependencyProperty.RegisterAttached("PreviouslyAttached", typeof(bool), typeof(AttachedProperties), new PropertyMetadata(default(bool)));

        public static void SetPreviouslyAttached(DependencyObject element, bool value)
        {
            element.SetValue(PreviouslyAttachedProperty, value);
        }

        public static bool GetPreviouslyAttached(DependencyObject element)
        {
            return (bool)element.GetValue(PreviouslyAttachedProperty);
        }

        #endregion
    }
}
