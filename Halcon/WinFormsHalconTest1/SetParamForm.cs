using HalconDotNet;
using System.Windows.Shapes;

namespace WinFormsHalconTest1
{
    public partial class SetParamForm : Form
    {
        Dictionary<string, HTuple> metrologyDic;
        HTuple metrologyHandle;

        public SetParamForm(HTuple metrologyHandle, Dictionary<string, HTuple> metrologyDic)
        {
            InitializeComponent();
            this.metrologyHandle = metrologyHandle;
            this.metrologyDic = metrologyDic;
            CmbMetrologyType.DataSource = metrologyDic.Keys.ToList();
            CmbMetrologyType.SelectedIndex = 0;
        }


        private void BtnAddParam_Click(object sender, EventArgs e)
        {
            string type = CmbMetrologyType.Text;
            double len1 = double.Parse(TxtLen1.Text);
            double len2 = double.Parse(TxtLen2.Text);
            double sigama = double.Parse(TxtSigama.Text);
            double contrant = double.Parse(txtContrant.Text);


            HOperatorSet.AddMetrologyObjectGeneric(metrologyHandle, type, metrologyDic[type], len1, len2, sigama, contrant, new HTuple(), new HTuple(), out HTuple index);
            MessageBox.Show("测量句柄参数添加成功");
        }

        private void BtnParamClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Dispose(true);
        }
    }
}
