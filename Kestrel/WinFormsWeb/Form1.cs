using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text;
using Zack.ComObjectHelpers;

namespace WinFormsWeb
{
    public partial class Form1 : Form
    {
        private IWebHost webhost;

        private COMReferenceTracker comRef = new COMReferenceTracker();
        dynamic presentation = null;

        public Form1()
        {
            InitializeComponent();

            this.webhost = new WebHostBuilder()
                .UseKestrel()
                .Configure(ConfigureWeb)
                .UseUrls("http://*.80")
                .Build();
            this.webhost.RunAsync();//一部运行Kestrel Web服务

            this.FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object? sender, FormClosedEventArgs e)
        {
            this.webhost.StopAsync();
            this.webhost.WaitForShutdown();
            comRef.Dispose();
        }

        private void ConfigureWeb(IApplicationBuilder app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.Run(AppRun);
        }

        //所有的非静态请求由AppRun处理
        private async Task AppRun(HttpContext context)
        {
            var req = context.Request;
            var resp = context.Response;
            string path = req.Path.Value;
            if (path == "/hello")
            {
                resp.StatusCode = 200;
                await resp.WriteAsync("hello wang");
            }
            else if (path == "/who")
            {
                resp.StatusCode = 200;
                await resp.WriteAsync("hello who? long");
            }
            else
            {
                resp.StatusCode = 404;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        dynamic T(dynamic obj)
        {
            return comRef.T(obj);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dynamic app = T(PowerPointHelper.CreatePowerPointApplication());
            app.Visible = true;

            dynamic presentations = T(app.Presentations);
            string pathFile = @"C:\Users\10844\Desktop\新建 PPTX 演示文稿.pptx";
            presentation = T(presentations.Open(pathFile));
            T(presentation.SlideShowSettings).Run();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            dynamic notesPate = T(T(T(this.presentation.SlideShowWindow).View).Slide).NotesPage;
            string notesText = GetInnerText(notesPate);
            MessageBox.Show(notesText);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            T(T(this.presentation.SlideShowWindow).View).Previous();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            T(T(this.presentation.SlideShowWindow).View).Next();
        }

        private string GetInnerText(dynamic part)
        { 
            StringBuilder sb = new StringBuilder();
            dynamic shapes = T(T(part.Shapes));
            int shapesCount = shapes.Count;
            for (int i = 0; i < shapesCount; i++) 
            {
                dynamic shape = T(shapes[i + 1]);
                var textFrame = T(shape.TextFrame);
                if (textFrame.HasText == -1)
                {
                    string text = T(textFrame.TextRange).Text;
                    sb.Append(text);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

    }
}