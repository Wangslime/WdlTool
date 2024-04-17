using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Newtonsoft.Json;
using static System.Text.Json.JsonElement;

namespace WpfApp1.DrUsercont
{
    /// <summary>
    /// InBeltParamUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class InBeltParamUserControl : UserControl
    {
        public InBeltParamUserControl()
        {
            InitializeComponent();

            
            A a  = new A();
            a.Add(3);
        }

        public class A
        {

            public void Add<T>(T t)
            { 
                Type type = typeof(T);
                Type type1 = t.GetType();
            }
        }
    }
}
