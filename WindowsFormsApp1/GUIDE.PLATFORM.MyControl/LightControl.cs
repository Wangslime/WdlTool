using GUIDE.PLATFORM.MyControl.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GUIDE.PLATFORM.MyControl
{
    public partial class LightControl : Control
    {
        public LightControl()
        {
            InitializeComponent();
        }

        public LightControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private LightType _lightType = LightType.Lock;
        [Description("选择显示图片类型")]
        [DefaultValue(true)]
        public LightType lightType
        {
            get
            {
                return _lightType;
            }
            set
            {
                _lightType = value;
                Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Bitmap bitmap = null;
            string text = "";

            switch (lightType)
            {
                case LightType.None:
                    break;
                case LightType.LightOn:
                    bitmap = Resources.Light_Bridge_On;
                    text = "灯光开";
                    break;
                case LightType.LightOff:
                    bitmap = Resources.Light_Bridge_Off;
                    text = "灯光关";
                    break;
                case LightType.Lock:
                    bitmap = Resources.Light_Lock_On;
                    text = "闭锁";
                    break;
                case LightType.UnLock:
                    bitmap = Resources.Light_UnLock_On;
                    text = "开锁";
                    break;
                case LightType.CntrTouchOn:
                    bitmap = Resources.Light_Touch_On;
                    text = "着箱";
                    break;
                case LightType.CntrTouchOff:
                    bitmap = Resources.Light_Touch_Off;
                    text = "未着箱";
                    break;
            }

            Graphics g = e.Graphics;
            if (bitmap != null)
            {
                g.DrawImage(bitmap, new Rectangle(0, 0, Width, (int)(Height * 0.7f)));
                Font font = new Font("微软雅黑", 12, FontStyle.Regular);
                StringFormat format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near
                };
                g.DrawString(text, font, Brushes.White, new Rectangle(0, (int)(Height * 0.7f), Width, (int)(Height * 0.3f)), format);
                font.Dispose();
                format.Dispose();
            }
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
