using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GUIDE.PLATFORM.MyControl
{
    public partial class LineControl : Control
    {
        public LineControl()
        {
            InitializeComponent();
        }

        public LineControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        
        private Color _lineColor = Color.Transparent;
        [Description("选择画线颜色")]
        [DefaultValue(true)]
        public Color lineColor
        {
            get
            {
                return _lineColor;
            }
            set
            {
                if (_lineColor != value)
                {
                    _lineColor = value;
                    Invalidate();
                }
            }
        }
        private bool _horizontal = false;
        [Description("是否是横线")]
        [DefaultValue(true)]
        public bool horizontal
        {
            get
            {
                return _horizontal;
            }
            set
            {
                if (_horizontal != value)
                {
                    _horizontal = value;
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Pen SpreaderLinePen = new Pen(lineColor, 2);
            if (horizontal)
            {
                g.DrawLine(SpreaderLinePen, 0, 0, Width, 0);
            }
            else 
            {
                g.DrawLine(SpreaderLinePen, 0, 0, 0, Height);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
