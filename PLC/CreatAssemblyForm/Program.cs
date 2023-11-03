using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace CreatAssemblyForm
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Assembly assembly = AssemblyBuilder.Load("DynamicAssemblyExample");
            Type[] type = assembly.GetTypes();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
