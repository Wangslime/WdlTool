using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DeletePath
{
    /// <summary>
    /// Message.xaml 的交互逻辑
    /// </summary>
    public partial class MessageWindow : Window
    {
        //private string _message;

        //public string Message
        //{
        //    get { return _message; }
        //    set { _message = value; }
        //}


        public MessageWindow(string message,string title="")
        {
            InitializeComponent();
            Message.Text = message;
            this.Title = title;
        }
    }
}
