using Drsoft.Tools.PdfLibrary;
using System;
using System.IO;
using System.Reflection;
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

namespace Drsoft.Tools.PdfDecryptWpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    #region 加密

    private void EncryptPath_Button_Click(object sender, RoutedEventArgs e)
    {
        System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
        openFile.Filter = "PDF文件|*.pdf|所有文件|*.*";
        if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            EncryptPdfPath.Text = openFile.FileName;
        }
    }

    private void Encrypt_Button_Click(object sender, RoutedEventArgs e)
    {
        string path = EncryptPdfPath.Text;
        if (path.EndsWith(".pdf") || path.EndsWith(".PDF"))
        {
            try
            {
                Pdfcrypt.Encrypt(path);
                if (File.Exists(path.Replace(".pdf", "_Dr.pdf")) || File.Exists(path.Replace(".PDF", "_Dr.PDF")))
                {
                    MessageBox.Show($"生成加密pdf文件成功！！！\r\n文件路径：{path.Replace(".pdf", "_Dr.pdf")}");
                }
                else
                {
                    MessageBox.Show("生成加密pdf文件失败，可能源文件被占用！！！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.StackTrace}");
            }
        }
        else
        {
            MessageBox.Show("请选择pdf文件！！！");
        }
    }

    #endregion


    #region 解密

    private void DecryptPath_Button_Click(object sender, RoutedEventArgs e)
    {
        System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
        openFile.Filter = "PDF文件|*.pdf|所有文件|*.*";
        if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            DecryptPdfPath.Text = openFile.FileName;
        }
    }


    private void Decrypt_Button_Click(object sender, RoutedEventArgs e)
    {
        string path = DecryptPdfPath.Text;
        if (path.EndsWith(".pdf") || path.EndsWith(".PDF"))
        {
            try
            {
                PdfPageView pdfPageView = new PdfPageView(path);
                pdfPageView.WindowState = System.Windows.WindowState.Maximized;
                pdfPageView.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.StackTrace}");
            }
        }
        else
        {
            MessageBox.Show("请选择pdf文件！！！");
        }
        
    }
    #endregion
}