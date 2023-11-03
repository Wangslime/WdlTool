using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace CreatAssemblyForm
{
    public partial class Form1 : Form
    {
        ConfigJsonInfo config = new ConfigJsonInfo();
        string path = Application.StartupPath + "\\appconfig.json";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            string strConfig = File.ReadAllText(path);
            config = JsonConvert.DeserializeObject<ConfigJsonInfo>(strConfig);
            TxtAssemblyName.Text = config.CreatAssemblyName;
            TxtLoadExcel.Text = config.LoadExcelPath;
            TxtSaveDllPath.Text = config.SaveAssemblyPath;
        }

        private void BtnSaveDllPath_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "所有文件 (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                TxtSaveDllPath.Text = openFileDialog.FileName;

                config.SaveAssemblyPath = openFileDialog.FileName;

                string strConfig = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(path, strConfig);
            }
        }

        private void BtnSetAssemblyName_Click(object sender, System.EventArgs e)
        {
            if (TxtAssemblyName.Text != config.CreatAssemblyName)
            {
                config.CreatAssemblyName = TxtAssemblyName.Text;
                string strConfig = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(path, strConfig);
            }
        }

        private void BtnLoadExcel_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "所有文件 (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                TxtLoadExcel.Text = openFileDialog.FileName;
                config.LoadExcelPath = openFileDialog.FileName;
                string strConfig = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(path, strConfig);
            }
        }

        private async void BtnCreatDll_Click(object sender, System.EventArgs e)
        {
            //CreatAssemblyObj creatAssemblyObj = new CreatAssemblyObj();
            //await creatAssemblyObj.Creat(config.LoadExcelPath,config.GlobalName, config.CreatAssemblyName);
        }
    }



    public class ConfigJsonInfo
    {
        public string GlobalName { get; set; }
        public string CreatAssemblyName { get; set; } = "";
        public string LoadExcelPath { get; set; } = "";
        public string SaveAssemblyPath { get; set; } = "";
    }
}
