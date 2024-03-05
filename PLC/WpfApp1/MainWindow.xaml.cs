using BeckhoffPLC;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ConcurrentDictionary<int, TaskCompletionSource<int>> dicDrMark = new ConcurrentDictionary<int, TaskCompletionSource<int>>();
        public MainWindow()
        {
            InitializeComponent();
            object[] paramObjs = new object[] { 1, "1", 1.0 };
            FD fD = new FD();
            fD.paramObjs = paramObjs;
            string msg = JsonConvert.SerializeObject(fD);
            fD = JsonConvert.DeserializeObject<FD>(msg);
        }

        public class FD
        { 
            public object[] paramObjs { get; set; }
        }

        private async Task Test1()
        {
            while (true)
            {
                var requestReceived = new TaskCompletionSource<int>();
                dicDrMark.TryAdd(1, requestReceived);
                int b = await requestReceived.Task;
                dicDrMark.TryRemove(1, out _);
                int c = b;
            }
        }

        public static async void GetLaserOffDelay()
        {
            string functionName = MethodBase.GetCurrentMethod().Name;
            int methodName = await GetCurrentMethodName();
        }

        public static async Task<int> GetCurrentMethodName([CallerMemberName] string methodName = "")
        {
            await Task.Delay(1000);
            return 3;
        }
    }
}
