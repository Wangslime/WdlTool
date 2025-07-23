using PdfiumViewer;
using System;
using System.IO;
using System.Windows;

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
            pdfViewer.ShowToolbar = false;
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
