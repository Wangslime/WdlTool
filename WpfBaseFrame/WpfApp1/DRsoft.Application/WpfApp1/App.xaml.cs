using System.Diagnostics;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            StartUp startUp = new StartUp();

            //全局异常捕获
            startUp.ApplicationException();

            //IOC容器对象配置注入，IOC容器为程序大脑总指挥，管理所有的对象
            startUp.IocConfigure();

            //使用Aop技术
            startUp.StartAop();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Process.GetCurrentProcess().Kill();
        }
    }
}
