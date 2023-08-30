using GUIDE.PLATFORM.MyControl.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIDE.PLATFORM.MyControl
{
    public partial class BridgeBaseMapControl : Control
    {
        private Region _region;
        public BridgeBaseMapControl()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        public BridgeBaseMapControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true); // 设置控件透明样式
            BackColor = Color.Transparent; // 设置背景颜色为透明
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            BackColor = Color.Transparent;
            e.Graphics.CompositingMode = CompositingMode.SourceOver; // 设置绘制模式为透明
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Bitmap leftLegBitMap = null;
            Bitmap rightLegBitMap = null;
            Bitmap beamBitMap = null;
            Bitmap bridgeFloorBitMap = null;
            int BeamHeight = ControlConfig.Instance.BeamHeight;
            int LeftLegStartPos = ControlConfig.Instance.LeftLegStartPos;

            switch (ControlConfig.Instance.skinStyle)
            {
                case SkinStyle.Black:
                    bridgeFloorBitMap = Resources.BridgeFloor_Dart;
                    break;
                case SkinStyle.Blue:
                    break;
            }
            switch (ControlConfig.Instance.bridgeColor)
            {
                case BridgeColor.Yellow:
                    leftLegBitMap = Resources.LiftLeg_Yellow;
                    rightLegBitMap = Resources.RightLeg_Yellow;
                    beamBitMap = Resources.beam_Yellow;
                    break;
                case BridgeColor.Green:
                    break;
                case BridgeColor.Blue:
                    break;
                case BridgeColor.Red:
                    break;
            }
            Rectangle beamFloor = new Rectangle(0, Height - BeamHeight, Width, BeamHeight);
            g.DrawImage(bridgeFloorBitMap, beamFloor);

            Rectangle beamRect = new Rectangle((int)(BeamHeight * 0.27f), 0, Width - (int)(BeamHeight * 0.54f), BeamHeight);
            g.DrawImage(beamBitMap, beamRect);

            int legWidth = BeamHeight;
            Rectangle leftLegRect = new Rectangle(LeftLegStartPos, BeamHeight - 2, legWidth, Height - BeamHeight + 2);
            g.DrawImage(leftLegBitMap, leftLegRect);

            Rectangle rightLegRect = new Rectangle(Width - LeftLegStartPos - legWidth, BeamHeight - 2, legWidth, Height - BeamHeight + 2);
            g.DrawImage(rightLegBitMap, rightLegRect);

            leftLegBitMap?.Dispose();
            rightLegBitMap?.Dispose();
            beamBitMap?.Dispose();
            bridgeFloorBitMap?.Dispose();
            g.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // 创建控件使用的 GraphicsPath 对象
            var path = new GraphicsPath();
            path.AddEllipse(new Rectangle(-5, -5, Width + 10, Height + 10));
            path.CloseAllFigures();

            // 将 GraphicsPath 对象转换为 Region 对象，并设置到该控件
            _region?.Dispose();
            _region = new Region(path);
            Region = _region;
        }
    }
}
