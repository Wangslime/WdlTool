using HalconDotNet;

namespace WinFormsHalconTest1
{
    public partial class MainForm : Form
    {
        HObject hImage = null;
        Dictionary<string, HTuple> metrologyDic = new Dictionary<string, HTuple>();
        HTuple metrologyHandle;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hsControl.HalconWindow.SetColor("red");
            hsControl.HalconWindow.SetLineWidth(3);
            hsControl.HalconWindow.SetDraw("margin");
        }

        /// <summary>
        /// 加载待测图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "图像类型 | *.jpg;*.png;*.bmp";
            dialog.DereferenceLinks = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = dialog.FileName;

                //创建一个Halcon图像对象
                hImage = new HImage(imagePath);

                //展示图像
                hsControl.HalconWindow.ClearWindow();
                hsControl.HalconWindow.DispObj(hImage);
                hsControl.SetFullImagePart();
            }
        }

        private void BtnDrawLine_Click(object sender, EventArgs e)
        {
            if (hImage == null)
            {
                MessageBox.Show("绘制之前，请先加载测量图片");
                return;
            }
            hsControl.Focus();
            Task.Run(() =>
            {
                //HOperatorSet.DrawLine(hsControl.HalconWindow, out double row1, out double column1, out double row2, out double column2);
                hsControl.HalconWindow.DrawLine(out double row1, out double column1, out double row2, out double column2);
                hsControl.HalconWindow.DispLine(row1, column1, row2, column2);
                metrologyDic.Add("line", new HTuple(row1, column1, row2, column2));
            });

        }

        private void BtnDrawCircle_Click(object sender, EventArgs e)
        {
            if (hImage == null)
            {
                MessageBox.Show("绘制之前，请先加载测量图片");
                return;
            }
            hsControl.Focus();
            Task.Run(() =>
            {
                hsControl.HalconWindow.DrawCircle(out double row, out double column, out double radius);
                hsControl.HalconWindow.DispCircle(row, column, radius);
                metrologyDic.Add("circle", new HTuple(row, column, radius));
            });
        }

        private void BtbEllipse_Click(object sender, EventArgs e)
        {
            if (hImage == null)
            {
                MessageBox.Show("绘制之前，请先加载测量图片");
                return;
            }
            hsControl.Focus();
            Task.Run(() =>
            {
                hsControl.HalconWindow.DrawEllipse(out double row, out double column, out double phi, out double radius1, out double radius2);
                hsControl.HalconWindow.DispEllipse(row, column, phi, radius1, radius2);
                metrologyDic.Add("ellipse", new HTuple(row, column, phi, radius1, radius2));
            });
        }

        private void BtnDrawRectangle_Click(object sender, EventArgs e)
        {
            if (hImage == null)
            {
                MessageBox.Show("绘制之前，请先加载测量图片");
                return;
            }
            hsControl.Focus();
            Task.Run(() =>
            {
                hsControl.HalconWindow.DrawRectangle2(out double row, out double column, out double phi, out double length1, out double length2);
                hsControl.HalconWindow.DispRectangle2(row, column, phi, length1, length2);
                metrologyDic.Add("rectangle2", new HTuple(row, column, phi, length1, length2));
            });
        }

        private void BtnCreatMetrologyModel_Click(object sender, EventArgs e)
        {
            //创建一个卡尺的测量模型
            HOperatorSet.CreateMetrologyModel(out metrologyHandle);

            //开始设置参数
            SetParamForm setParamForm = new SetParamForm(metrologyHandle, metrologyDic);
            setParamForm.ShowDialog();

        }

        private void BtnResult_Click(object sender, EventArgs e)
        {
            if (!metrologyDic.Any())
            {
                MessageBox.Show("请先设置测量参数后在执行");
                return;
            }

            for (int i = 0; i < metrologyDic.Count; i++)
            {
                
                HOperatorSet.GetMetrologyObjectModelContour(out HObject contour, metrologyHandle, i, 1.5);
                //获取轮廓线上的切片
                HOperatorSet.GetMetrologyObjectMeasures(out HObject contours, metrologyHandle, "all", "all", out _, out _);

                //显示轮廓线和切片
                HOperatorSet.DispObj(contour, hsControl.HalconWindow);
                HOperatorSet.DispObj(contours, hsControl.HalconWindow);

                //执行测量
                HOperatorSet.ApplyMetrologyModel(hImage, metrologyHandle);

                //获取执行结果
                HOperatorSet.GetMetrologyObjectResultContour(out HObject contourResult, metrologyHandle, i, "all", 1.5);
                HOperatorSet.SetColor(hsControl.HalconWindow, "red");
                HOperatorSet.DispObj(contourResult, hsControl.HalconWindow);
            }
        }
    }
}
