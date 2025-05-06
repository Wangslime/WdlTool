using System.ComponentModel;
using System.Windows;

namespace DRSoft.Runtime.MVVM.Toolkit
{
    public class Execute
    {
        public static bool InDesignMode
        {
            get { return DesignerProperties.GetIsInDesignMode(new DependencyObject()); }
        }
    }
}
