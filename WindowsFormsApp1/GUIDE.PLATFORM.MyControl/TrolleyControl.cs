using GUIDE.PLATFORM.MyControl.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GUIDE.PLATFORM.MyControl
{
    public partial class TrolleyControl : Control
    {
        public TrolleyControl()
        {
            InitializeComponent();
        }

        public TrolleyControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Bitmap bitmap = Resources.Trolley_Yellow;
            g.DrawImage(bitmap, 0, 0, Width, Height);
            bitmap?.Dispose();
            g.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
