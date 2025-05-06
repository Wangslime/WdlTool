using System.Windows;

namespace DRSoft.Runtime.MVVM.Toolkit
{
    public abstract class BootstrapperBase
    {
        public void Initialize()
        {
            Configure();
            if (!Execute.InDesignMode)
            {
                Application application = Application.Current;
                application.Startup += OnStartup;
                application.Exit += OnExit;
            }
        }
        protected virtual void Configure()
        {
        }
        protected virtual void OnStartup(object sender, StartupEventArgs e)
        {
        }
        protected virtual void OnExit(object sender, EventArgs e)
        {
        }
    }
}
