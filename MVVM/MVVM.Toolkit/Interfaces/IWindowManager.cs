using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DRSoft.Runtime.MVVM.Toolkit.Interfaces
{
    public interface IWindowManager
    {
        public Task<bool?> ShowDialogAsync(object rootModel, object? context = null, IDictionary<string, object> settings = null);

        public Task ShowWindowAsync(object rootModel, object? context = null, IDictionary<string, object>? settings = null);

        /// <summary>
        /// Shows a popup at the current mouse position.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The view context.</param>
        /// <param name="settings">The optional popup settings.</param>
        public Task ShowPopupAsync(object rootModel, object? context = null, IDictionary<string, object>? settings = null);
    }
}
