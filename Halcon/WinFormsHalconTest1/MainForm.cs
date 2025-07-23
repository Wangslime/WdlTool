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
        /// ���ش���ͼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "ͼ������ | *.jpg;*.png;*.bmp";
            dialog.DereferenceLinks = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = dialog.FileName;

                //����һ��Halconͼ�����
                hImage = new HImage(imagePath);

                //չʾͼ��
                hsControl.HalconWindow.ClearWindow();
                hsControl.HalconWindow.DispObj(hImage);
                hsControl.SetFullImagePart();
            }
        }

        private void BtnDrawLine_Click(object sender, EventArgs e)
        {
            if (hImage == null)
            {
                MessageBox.Show("����֮ǰ�����ȼ��ز���ͼƬ");
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
                MessageBox.Show("����֮ǰ�����ȼ��ز���ͼƬ");
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
                MessageBox.Show("����֮ǰ�����ȼ��ز���ͼƬ");
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
                MessageBox.Show("����֮ǰ�����ȼ��ز���ͼƬ");
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
            //����һ�����ߵĲ���ģ��
            HOperatorSet.CreateMetrologyModel(out metrologyHandle);

            //��ʼ���ò���
            SetParamForm setParamForm = new SetParamForm(metrologyHandle, metrologyDic);
            setParamForm.ShowDialog();

        }

        private void BtnResult_Click(object sender, EventArgs e)
        {
            if (!metrologyDic.Any())
            {
                MessageBox.Show("�������ò�����������ִ��");
                return;
            }

            for (int i = 0; i < metrologyDic.Count; i++)
            {
                
                HOperatorSet.GetMetrologyObjectModelContour(out HObject contour, metrologyHandle, i, 1.5);
                //��ȡ�������ϵ���Ƭ
                HOperatorSet.GetMetrologyObjectMeasures(out HObject contours, metrologyHandle, "all", "all", out _, out _);

                //��ʾ�����ߺ���Ƭ
                HOperatorSet.DispObj(contour, hsControl.HalconWindow);
                HOperatorSet.DispObj(contours, hsControl.HalconWindow);

                //ִ�в���
                HOperatorSet.ApplyMetrologyModel(hImage, metrologyHandle);

                //��ȡִ�н��
                HOperatorSet.GetMetrologyObjectResultContour(out HObject contourResult, metrologyHandle, i, "all", 1.5);
                HOperatorSet.SetColor(hsControl.HalconWindow, "red");
                HOperatorSet.DispObj(contourResult, hsControl.HalconWindow);
            }
        }
    }
}
