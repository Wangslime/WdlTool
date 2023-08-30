using GUIDE.PLATFORM.MyControl.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIDE.PLATFORM.MyControl
{
    public partial class RscButton : Button
    {
        public RscButton()
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = Color.FromArgb(115, 122, 142);
            this.FlatAppearance.BorderSize = 1;
        }

        private RscButtonType _buttonType = RscButtonType.Auto;
        [Description("选择显示图片类型")]
        [DefaultValue(true)]
        public RscButtonType buttonType
        {
            get
            {
                return _buttonType;
            }
            set
            {
                _buttonType = value;
                if (!string.IsNullOrEmpty(this.Text))
                {
                    this.Text = string.Empty;
                }
                GetBackGroudImage(value);
            }
        }

        private void GetBackGroudImage(RscButtonType rscButtonType, bool isClick = false)
        {
            Bitmap bitmap = null;
            switch (rscButtonType)
            {
                case RscButtonType.None:
                    break;
                case RscButtonType.Auto:
                    bitmap = Resources.Button_Auto;
                    break;
                case RscButtonType.HalfAuto:
                    bitmap = Resources.Button_HalfAuto;
                    break;
                case RscButtonType.Manual:
                    bitmap = Resources.Button_Manual;
                    break;
                case RscButtonType.AutoRun:
                    bitmap = Resources.Button_AutoRun;
                    break;
                case RscButtonType.TerminalRun:
                    bitmap = isClick ? Resources.Button_TerminalRun_On : Resources.Button_TerminalRun_Off;
                    break;
                case RscButtonType.TierCheck:
                    bitmap = Resources.Button_TierCheck;
                    break;
                case RscButtonType.TaskRollBack:
                    bitmap = isClick ? Resources.TaskRollBack_On : Resources.TaskRollBack_Off;
                    break;
                case RscButtonType.More:
                    bitmap = Resources.Button_More;
                    break;
            }
            this.BackgroundImage = bitmap;

            if (rscButtonType == RscButtonType.Auto)
            {
                if (this.FlatAppearance.BorderColor != Color.FromArgb(3, 238, 135))
                {
                    this.FlatAppearance.BorderColor = Color.FromArgb(3, 238, 135);
                }
                
            }
            else if (rscButtonType == RscButtonType.HalfAuto)
            {
                if (this.FlatAppearance.BorderColor != Color.FromArgb(27, 147, 228))
                {
                    this.FlatAppearance.BorderColor = Color.FromArgb(27, 147, 228);
                }
            }
            else
            {
                if (this.FlatAppearance.BorderColor != Color.FromArgb(115, 122, 142))
                {
                    this.FlatAppearance.BorderColor = Color.FromArgb(115, 122, 142);
                }
            }
        }

        protected override async void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Left)
            {
                if (buttonType == RscButtonType.TerminalRun || buttonType == RscButtonType.TaskRollBack)
                {
                    GetBackGroudImage(buttonType, true);
                    await Task.Delay(200);
                    GetBackGroudImage(buttonType);
                }
            }
        }

        public RscButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
