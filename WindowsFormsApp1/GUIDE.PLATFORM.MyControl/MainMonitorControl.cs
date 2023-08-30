using System;
using System.Windows.Forms;

namespace GUIDE.PLATFORM.MyControl
{
    public partial class MainMonitorControl : UserControl
    {
        public MainMonitorControl()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //lightControl.lineHeight = (int)(Height / 7.7f);
        }
    }
}
