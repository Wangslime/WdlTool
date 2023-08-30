using System.Windows.Forms;

namespace GUIDE.PLATFORM.MyControl
{
    public partial class PosValueControls : UserControl
    {
        #region 单机监控界面上的Label赋值
        /// <summary>
        /// 当前大车位置
        /// </summary>
        public string CartPos
        {
            get { return lblCartPos.Text; }
            set { lblCartPos.Text = value; }
        }
        /// <summary>
        /// 当前小车位置
        /// </summary>
        public string TrolleyPos
        {
            get { return lblTrolleyPos.Text; }
            set { lblTrolleyPos.Text = value; }
        }
        /// <summary>
        /// 当前吊具位置
        /// </summary>
        public string SpreaderPos
        {
            get { return lblSpreaderPos.Text; }
            set { lblSpreaderPos.Text = value; }
        }
        /// <summary>
        /// 目标大车位置和大车差值
        /// </summary>
        public string CartTargetPos
        {
            get { return lblCartTargetPos.Text; }
            set
            {
                lblCartTargetPos.Text = value;
                if (string.IsNullOrEmpty(value))
                {
                    lblCartPlus.Text = "";
                }
                else
                {
                    if (int.TryParse(value, out int nCartTargetPos) && int.TryParse(CartPos, out int nCartCurrentPos))
                    {
                        lblCartPlus.Text = (nCartTargetPos - nCartCurrentPos).ToString();
                    }
                    else
                    {
                        lblCartPlus.Text = "";
                    }
                }
            }
        }
        /// <summary>
        /// 目标小车位置和小车差值
        /// </summary>
        public string TrolleyTargetPos
        {
            get { return lblTrolleyTargetPos.Text; }
            set
            {
                lblTrolleyTargetPos.Text = value;
                if (string.IsNullOrEmpty(value))
                {
                    lblTrolleyPlus.Text = "";
                }
                else
                {
                    if (int.TryParse(value, out int nTrolleyTargetPos) && int.TryParse(TrolleyPos, out int nTrolleyCurrentPos))
                    {
                        lblTrolleyPlus.Text = (nTrolleyTargetPos - nTrolleyCurrentPos).ToString();
                    }
                    else
                    {
                        lblTrolleyPlus.Text = "";
                    }
                }
            }
        }
        /// <summary>
        /// 目标吊具位置和吊具差值
        /// </summary>
        public string SpreaderTargetPos
        {
            get { return lblSpreaderTargetPos.Text; }
            set
            {
                lblSpreaderTargetPos.Text = value;
                if (string.IsNullOrEmpty(value))
                {
                    lblSpreadPlus.Text = "";
                }
                else
                {
                    if (int.TryParse(value, out int nSpreaderTargetPos) && int.TryParse(SpreaderPos, out int nSpreaderCurrentPos))
                    {
                        lblSpreadPlus.Text = (nSpreaderTargetPos - nSpreaderCurrentPos).ToString();
                    }
                    else
                    {
                        lblSpreadPlus.Text = "";
                    }
                }
            }
        }
        #endregion
        public PosValueControls()
        {
            InitializeComponent();
        }
    }
}