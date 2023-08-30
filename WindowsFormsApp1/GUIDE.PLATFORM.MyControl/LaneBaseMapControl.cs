using GUIDE.PLATFORM.MyControl.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIDE.PLATFORM.MyControl
{
    public partial class LaneBaseMapControl : UserControl
    {
        Color fontColor = Color.FromArgb(172, 187, 230);
        Color lineColor = Color.FromArgb(115, 125, 153);
        Color tier1Color = Color.FromArgb(176, 212, 104);
        Color tier2Color = Color.FromArgb(44, 201, 208);
        Color tier3Color = Color.FromArgb(56, 152, 210);
        Color tier4Color = Color.FromArgb(214, 97, 169);
        Color tier5Color = Color.FromArgb(255, 78, 87);
        Color tier6Color = Color.FromArgb(204, 31, 31);

        Color mainBridgeColor = Color.FromArgb(242, 178, 16);
        Color otherBridgeColor = Color.FromArgb(136, 136, 136);

        private int _bayCount = 100;
        [Description("设置总贝数量")]
        [DefaultValue(true)]
        public int bayCount
        {
            get
            {
                return _bayCount;
            }
            set
            {
                if (value != _bayCount)
                {
                    _bayCount = value;
                    Invalidate();
                }
            }
        }

        private int _rowCount = 9;
        [Description("设置总贝数量")]
        [DefaultValue(true)]
        public int rowCount
        {
            get
            {
                return _rowCount;
            }
            set
            {
                if (_rowCount != value)
                {
                    _rowCount = value;
                    Invalidate();
                }
            }
        }

        private bool _isShowSlideWire = true;
        [Description("是否显示滑触线")]
        [DefaultValue(true)]
        public bool isShowSlideWire
        {
            get
            {
                return _isShowSlideWire;
            }
            set
            {
                if (value != _isShowSlideWire)
                {
                    _isShowSlideWire = value;
                    Invalidate();
                }
            }
        }

        public LaneBaseMapControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            base.OnPaint(e);

            int upSpace = 30;
            int downSpace = 50;
            int broadSpace = 30;
            Rectangle rectangle = new Rectangle(Location.X + broadSpace, Location.Y + upSpace, Size.Width - broadSpace, Size.Height - upSpace - downSpace);

            SolidBrush fontBrush = new SolidBrush(fontColor);
            SolidBrush lineBrush = new SolidBrush(lineColor);
            Pen pen = new Pen(lineBrush);
            Font font = new Font("微软雅黑", 9, FontStyle.Regular);
            StringFormat format = new StringFormat()
            { 
                Alignment= StringAlignment.Center,
                LineAlignment= StringAlignment.Center,
            };

            int bayCount = this.bayCount;
            bayCount = (bayCount + 1) / 2;

            int rowHeight = rectangle.Height / rowCount;
            int bayWidth = rectangle.Width / bayCount;
            g.DrawRectangle(pen, new Rectangle(rectangle.X, rectangle.Y, bayWidth * bayCount, rowHeight * rowCount));
            for (int i = 0; i < rowCount; i++)
            {
                int height = rowHeight * i;
                g.DrawString((i * 2 + 1).ToString(), font, fontBrush, new Rectangle(this.Location.Y, rectangle.Y + height, broadSpace, rowHeight), format);
                if (i > 0)
                {
                    g.DrawLine(pen, rectangle.X, rectangle.Y + height, rectangle.X + bayWidth * bayCount, rectangle.Y + height);
                }
            }
            for (int i = 0; i < bayCount; i++)
            {
                int width = bayWidth * i;
                g.DrawString((i * 2 + 1).ToString(), font, fontBrush, new Rectangle(rectangle.X + width, this.Location.Y, bayWidth, upSpace / 2), format);
                if (i > 0)
                {
                    g.DrawLine(pen, rectangle.X + width, rectangle.Y, rectangle.X + width, rectangle.Y + rowHeight * rowCount);
                }
            }
            if (isShowSlideWire)
            {
                g.DrawImage(Resources.SlideWire, new Rectangle(rectangle.X, this.Location.Y + upSpace / 2, bayWidth * bayCount, upSpace / 2));
            }

            format.Dispose();
            font.Dispose();
            fontBrush?.Dispose();
            lineBrush?.Dispose();
            pen?.Dispose();
            g?.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
