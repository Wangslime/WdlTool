using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string sql =  textBox1.Text;
            int count = int.Parse(textBox1.Text);
            int number = int.Parse(textBox2.Text);

            
            for (int i = 0; i < count; i++)
            {
                sql.Replace("NIIJ", (number + 1).ToString());
                // do连接数据库执行sql语句

            }
        }
    }
}
