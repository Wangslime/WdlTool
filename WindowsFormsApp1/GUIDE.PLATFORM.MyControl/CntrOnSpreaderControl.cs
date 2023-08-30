using GUIDE.PLATFORM.MyControl.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIDE.PLATFORM.MyControl
{
    public partial class CntrOnSpreaderControl : Control
    {
        public CntrOnSpreaderControl()
        {
            InitializeComponent();
        }

        public CntrOnSpreaderControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private LockType _lockType = LockType.Unlock;
        [Description("吊具开闭所状态")]
        [DefaultValue(true)]
        public LockType lockType
        {
            get
            {
                return _lockType;
            }
            set
            {
                if (_lockType != value)
                {
                    _lockType = value;
                    Invalidate();
                }
            }
        }

        private ContainerType _containerType = ContainerType.GpEmpty;
        [Description("选择显示箱类型")]
        [DefaultValue(true)]
        public ContainerType containerType
        {
            get
            {
                return _containerType;
            }
            set
            {
                if (_containerType != value)
                {
                    _containerType = value;
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (lockType == LockType.Lock)
            {
                Graphics g = e.Graphics;

                Bitmap bitmap = null;

                switch (containerType)
                {
                    case ContainerType.GpEmpty:
                        bitmap = Resources.Container_Gp_Empty;
                        break;
                    case ContainerType.GpFull:
                        bitmap = Resources.Container_Gp_Full;
                        break;
                    case ContainerType.HqEmpty:
                        bitmap = Resources.Container_Hq_Empty;
                        break;
                    case ContainerType.HqFull:
                        bitmap = Resources.Container_Hq_Full;
                        break;
                    default:
                        break;
                }
                g.DrawImage(bitmap, 0, 0, Width, Height);
                g.Dispose();
                bitmap?.Dispose();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
