using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeletePath
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string deleteRecord = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextBoxFilePath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            deleteRecord = "";
            bool dirExists = Directory.Exists(TextBoxFilePath.Text);
            if (dirExists)
            {
                //string deletePath = TextBoxFilePath.Text;
                //string searchFilePattern = ".pdb";
                //string[] files1 = Directory.GetFiles(deletePath, searchFilePattern, SearchOption.AllDirectories);
                //string searchDirectoryPattern1 = "bin";
                //string[] dirs1 = Directory.GetDirectories(deletePath, searchDirectoryPattern1, SearchOption.AllDirectories);
                //string searchDirectoryPattern2 = "obj";
                //string[] dirs2 = Directory.GetDirectories(deletePath, searchDirectoryPattern1, SearchOption.AllDirectories);


                string deletePath = TextBoxFilePath.Text;
                DirectoryInfo deleteDir = new DirectoryInfo(deletePath);
                DeleteRecursion(deleteDir);
                //System.Windows.MessageBox.Show(deleteRecord, "删除信息");
                MessageWindow messageWindow = new MessageWindow(deleteRecord, "删除信息");
                messageWindow.Show();
            }
            else 
            {
                System.Windows.MessageBox.Show("输入路径无效！！！" , "警告");
            }
        }

        public void DeleteRecursion(DirectoryInfo deleteDir) 
        {
            bool binDirChecked = CheckBoxBIN.IsChecked.GetValueOrDefault(false);
            bool objDirChecked = CheckBoxOBJ.IsChecked.GetValueOrDefault(false);
            bool pdbFileChecked = CheckBox_PDB.IsChecked.GetValueOrDefault(false);

            
            List<FileSystemInfo> subDeleteList = new List<FileSystemInfo>(deleteDir.GetFileSystemInfos());  //返回deletePath目录下所有文件夹和子目录

            for (int i = 0; i < subDeleteList.Count; i++)
            {
                FileSystemInfo subDelete = subDeleteList[i];
                //判断是否文件夹
                if (subDelete is DirectoryInfo)
                {
                    //指定文件夹，则删除
                    if (subDelete.Name.Equals("bin") || subDelete.Name.Equals("obj"))
                    {
                        if (subDelete.Name.Equals("bin") && binDirChecked)
                        {
                            DeleteDir(subDelete);
                            deleteRecord += subDelete.FullName + "\n";
                        }

                        if (subDelete.Name.Equals("obj") && objDirChecked)
                        {
                            DeleteDir(subDelete);
                            deleteRecord += subDelete.FullName + "\n";
                        }

                        continue;
                    }
                    //其他文件夹，则继续递归
                    else 
                    {
                        DeleteRecursion((DirectoryInfo)subDelete);
                    }
                }
                else
                {
                    string lastFourChars = subDelete.Name.Length >= 4 ? subDelete.Name.Substring(subDelete.Name.Length - 4) : "";

                    if (lastFourChars.Equals(".pdb") && pdbFileChecked) 
                    {
                        DeleteFile(subDelete);
                        deleteRecord += subDelete.FullName + "\n";
                    }
                }
            }
        }

        /// <summary>
        /// 删除目录和目录下的所有文件
        /// </summary>
        /// <param name="subDelete"></param>
        public void DeleteDir(FileSystemInfo subDelete) 
        {
            DirectoryInfo subdir = new DirectoryInfo(subDelete.FullName);
            subdir.Delete(true);                  
        }

        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="subDelete"></param>
        public void DeleteFile(FileSystemInfo subDelete) 
        {
            File.Delete(subDelete.FullName);      //删除指定文件
        }
    }
}