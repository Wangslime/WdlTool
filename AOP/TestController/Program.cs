using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Reflection;
using System.Text;
using WdlProxyAOP;

namespace TestController
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new AopExecuted();

            AopEvent<ClassInterface>.Instance.BeForeExecuted += OnAopBeFore;
            AopEvent<ClassInterface>.Instance.AfterExecuted += OnAopAfter;
            AopEvent<ClassInterface>.Instance.ExceptionExecuted += OnException;
            try
            {
                ClassInterface Class1 = new Class1();
                Class1 = ProxyFactory.Creat(Class1);
                Class1.Add();
                Class1.TestError();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"发生错误：{ex.Message}");
                Console.ResetColor();
            }

            Console.ReadKey();
        }

        private static void OnAopBeFore(ClassInterface arg1, MethodInfo arg2)
        {
            
        }

        private static void OnAopAfter(ClassInterface arg1, MethodInfo arg2, object arg3)
        {
            
        }

        private static void OnException(ClassInterface arg1, MethodInfo arg2, Exception arg3)
        {
            
        }
    }

    public class AopExecuted
    {
        public AopExecuted()
        {
            AopEvent<ClassInterface>.Instance.BeForeExecuted += OnAopBeFore;
            AopEvent<ClassInterface>.Instance.AfterExecuted += OnAopAfter;
            AopEvent<ClassInterface>.Instance.ExceptionExecuted += OnException;
        }

        private void OnException(ClassInterface arg1, MethodInfo arg2, Exception arg3)
        {
            
        }

        private void OnAopAfter(ClassInterface arg1, MethodInfo arg2, object arg3)
        {
            
        }

        private void OnAopBeFore(ClassInterface arg1, MethodInfo arg2)
        {
            
        }
    }

    public interface ClassInterface
    {
        public int Add();
        public void TestError();
    }

    [ActionErrorFilter]
    public class Class1: ClassInterface
    {
        public int Add()
        {
            return 1;
        }

        [ActionExecutedFilter]
        [ActionExecutingFilter]
        public void TestError()
        { 
            throw new Exception("TestError");
        }
    }


    public class Test
    {
        public void Test1()
        {
            //string csvFilePath = "D:\\DrLaser\\Git\\TCSE_BC_Beckoff\\DRsoft.Application\\TCSE_XBC\\Assets\\Language\\Languages.csv";
            //List<CsvClass> csvClasses = new List<CsvClass>();
            //using (StreamReader reader = new StreamReader(csvFilePath, Encoding.GetEncoding("gb2312")))
            //{
            //    using (TextFieldParser parser = new TextFieldParser(reader))
            //    {
            //        parser.TextFieldType = FieldType.Delimited;
            //        parser.SetDelimiters(","); // 设置分隔符
            //        parser.HasFieldsEnclosedInQuotes = true; // 设置为true以处理带引号的字段

            //        while (!parser.EndOfData)
            //        {
            //             读取一行数据
            //            string[] fields = parser.ReadFields();

            //            string key = fields[0];
            //            string ch = fields[1];
            //            string en = fields[2];
            //            string a = ($"key:{key},  ch:{ch},  ch:{en}");
            //            csvClasses.Add(new CsvClass() { key = key, Ch = ch, En = en });
            //            NLogger.Instance.DataBase(a);
            //        }
            //    }
            //}

            //string filePath = "D:\\DrLaser\\Git\\TCSE_BC_Beckoff\\DRsoft.Bussiness\\DRsoft.Engine.Model\\Engine\\Params\\ParamJson.cs";
            //string[] lines = File.ReadAllLines(filePath, Encoding.GetEncoding("gb2312"));
            //List<string> lines1 = new List<string>();
            //foreach (string line in lines)
            //{
            //    string line1 = line;
            //    if (line.Contains("[Description("))
            //    {
            //        int index1 = line.IndexOf('(');
            //        int index2 = line.IndexOf(')');
            //        string des = line.Substring(index1 + 2, index2 - index1 - 3);
            //        string? key = null;
            //        try
            //        {
            //            key = csvClasses.SingleOrDefault(p => p.Ch == des)?.key;
            //        }
            //        catch (Exception)
            //        {
            //        }
            //        if (!string.IsNullOrWhiteSpace(key))
            //        {
            //            line1 = line.Replace(des, key);
            //        }
            //    }
            //    lines1.Add(line1);
            //}

            //string filePath = "D:\\DrLaser\\Git\\TCSE_BC_Beckoff\\DRsoft.Bussiness\\DRsoft.Engine.Model\\Engine\\Params\\ParamJson.cs";
            //string[] lines = File.ReadAllLines(filePath, Encoding.GetEncoding("gb2312"));
            //List<string> lines1 = new List<string>();
            //for (int i = 0; i < lines.Length; i++)
            //{
            //    string line1 = lines[i];
            //    if (lines[i].Contains("[Description("))
            //    {
            //        int index1 = lines[i].IndexOf('(');
            //        int index2 = lines[i].IndexOf(')');
            //        string des = lines[i].Substring(index1 + 2, index2 - index1 - 3);
            //        string name = "";
            //        if (lines[i + 1].Contains("public class "))
            //        {
            //            name = lines[i + 1].TrimStart().Split(' ')[2];
            //        }
            //        else if (lines[i + 1].Contains("public float "))
            //        {
            //            name = lines[i + 1].TrimStart().Split(' ')[2];
            //        }
            //        else if (lines[i + 2].Contains("public float "))
            //        {
            //            name = lines[i + 2].TrimStart().Split(' ')[2];
            //        }
            //        else if (lines[i + 3].Contains("public float "))
            //        {
            //            name = lines[i + 3].TrimStart().Split(' ')[2];
            //        }
            //        else if (lines[i + 4].Contains("public float "))
            //        {
            //            name = lines[i + 4].TrimStart().Split(' ')[2];
            //        }
            //        if (!string.IsNullOrWhiteSpace(name))
            //        {
            //            line1 = lines[i].Replace(des, name);
            //        }
            //    }
            //    lines1.Add(line1);
            //}
            //filePath = "D:\\DrLaser\\Git\\TCSE_BC_Beckoff\\DRsoft.Bussiness\\DRsoft.Engine.Model\\Engine\\Params\\ParamJson1.cs";
            //File.WriteAllLines(filePath, lines1);

            //string filePath = "D:\\DrLaser\\Git\\TCSE_BC_Beckoff\\DRsoft.Bussiness\\DRsoft.Engine.Model\\Engine\\Params\\ParamJson.cs";
            //string[] lines = File.ReadAllLines(filePath, Encoding.GetEncoding("gb2312"));
            //List<string> lines1 = new List<string>();
            //Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            //// 打印读取到的内容
            //for (int i = 0; i < lines.Length; i++)
            //{
            //    if (lines[i].Contains("[Description("))
            //    {
            //        int index1 = lines[i].IndexOf('(');
            //        int index2 = lines[i].IndexOf(')');
            //        string des = lines[i].Substring(index1 + 2, index2 - index1 - 3);
            //        string name = "";
            //        if (lines[i + 1].Contains("public class "))
            //        {
            //            name = lines[i + 1].TrimStart().Split(' ')[2];
            //        }
            //        else if (lines[i + 1].Contains("public float "))
            //        {
            //            name = lines[i + 1].TrimStart().Split(' ')[2];
            //        }
            //        else if (lines[i + 2].Contains("public float "))
            //        {
            //            name = lines[i + 2].TrimStart().Split(' ')[2];
            //        }
            //        else if (lines[i + 3].Contains("public float "))
            //        {
            //            name = lines[i + 3].TrimStart().Split(' ')[2];
            //        }
            //        else if (lines[i + 4].Contains("public float "))
            //        {
            //            name = lines[i + 4].TrimStart().Split(' ')[2];
            //        }
            //        if (!string.IsNullOrWhiteSpace(name))
            //        {
            //            lines1.Add($"{name},{des},");
            //            keyValuePairs.Add(name, des);
            //        }
            //    }
            //}
            //filePath = "D:\\DrLaser\\Git\\TCSE_BC_Beckoff\\DRsoft.Bussiness\\DRsoft.Engine.Model\\Controller\\ParamJson1.csv";
            //File.WriteAllLines(filePath, lines1);
        }
    }
}