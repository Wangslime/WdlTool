using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Drsoft.Tools.PdfLibrary
{
    /// <summary>
    /// PdfPageView.xaml 的交互逻辑
    /// </summary>
    public partial class PdfPageView : Window
    {
        private string pdfNewPath = "";
        private PdfPageView() { }
        public PdfPageView(string path)
        {
            InitializeComponent();

            pdfNewPath = Pdfcrypt.Decrypt(path);
            PdfDocument document = PdfDocument.Load(pdfNewPath);
            pdfViewer.Document = document;
            this.Closed += PdfPageView_Closed;
        }

        private void PdfPageView_Closed(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(pdfNewPath))
            {
                if (File.Exists(pdfNewPath))
                {
                    pdfViewer?.Document?.Dispose();
                    pdfViewer?.Dispose();
                    pdfViewer = null;
                    File.Delete(pdfNewPath);
                }
            }
        }
    }
}
