using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfControlLibrary1
{
    /// <summary>
    /// CameraControl.xaml 的交互逻辑
    /// </summary>
    public partial class CameraControl : UserControl
    {
        public CancellationTokenSource cts = new CancellationTokenSource();

        public bool isConnect { get; set; }

        private CameraStatus _cameraStatus = CameraStatus.NotConnect;
        public CameraStatus cameraStatus
        { 
            get 
            { 
                return _cameraStatus; 
            }
            set 
            { 
                _cameraStatus = value;
                IsRefersh = true;
            }
        }

        private Color color = Color.FromArgb(180, 171, 158);

        private bool IsRefersh = true;
        public CameraControl()
        {
            InitializeComponent();
            Task.Factory.StartNew(async () => await RefershUiTask(), TaskCreationOptions.LongRunning);
        }

        private async Task RefershUiTask()
        {
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    if (IsRefersh)
                    {
                        if (canvas.ActualHeight > 0)
                        {
                            IsRefersh = false;
                            await DrawShapes();
                        }
                    }
                    await Task.Delay(50);
                }
                catch (Exception ex)
                {
                    await Task.Delay(1000);
                }
            }
        }

        public async Task DrawShapes()
        {
            Bitmap bitmap = new Bitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight);
            Graphics g = Graphics.FromImage(bitmap);
            // 开启抗锯齿
            g.SmoothingMode = SmoothingMode.AntiAlias;

            color = Color.FromArgb(1, 233, 250);
            switch (cameraStatus)
            {
                case CameraStatus.NotConnect:
                    color = Color.White;
                    break;
                case CameraStatus.Runing:
                    color = Color.FromArgb(0, 250, 175);
                    break;
                case CameraStatus.End:
                    break;
                case CameraStatus.Exception:
                    color = Color.Red;
                    break;
                default:
                    break;
            }

            // 创建圆角矩形路径
            int cornerRadius = 9;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, bitmap.Height * 0.2f, cornerRadius * 2, cornerRadius * 2, 180, 90); // 左上角
            path.AddArc(bitmap.Width - cornerRadius * 2, bitmap.Height * 0.2f, cornerRadius * 2, cornerRadius * 2, 270, 90); // 右上角
            path.AddArc(bitmap.Width - cornerRadius * 2, bitmap.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90); // 右下角
            path.AddArc(0, bitmap.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90); // 左下角
            path.CloseFigure(); // 闭合路径
            // 填充圆角矩形
            Brush brush = new SolidBrush(color);
            g.FillPath(brush, path);

            path.Dispose();
            path = new GraphicsPath();
            path.AddArc(bitmap.Width * 0.2f, 0, cornerRadius * 2, cornerRadius * 2, 180, 90); // 左上角
            path.AddArc(bitmap.Width*0.8f - cornerRadius * 2, 0, cornerRadius * 2, cornerRadius * 2, 270, 90); // 右上角
            path.AddArc(bitmap.Width * 0.8f - cornerRadius * 2, bitmap.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90); // 右下角
            path.AddArc(bitmap.Width * 0.2f, bitmap.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90); // 左下角
            path.CloseFigure(); // 闭合路径
            g.FillPath(brush, path);
           
            // 绘制圆形轮廓线
            Pen pen = new Pen(Color.FromArgb(12, 25, 56), 4);
            int centerX = bitmap.Width / 2 - 3 - 4;
            int centerY = bitmap.Height / 2 - 3;
            int radius = 6;
            g.DrawEllipse(pen, centerX, centerY, radius * 2, radius * 2);

            pen.Dispose();
            brush.Dispose();
            path.Dispose();

            await this.Dispatcher.BeginInvoke(() =>
            {
                BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                        bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                cameraImg.Source = bitmapSource;
            });
            bitmap?.Dispose();
            g?.Dispose();
        }
    }
}
