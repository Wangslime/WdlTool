using System.Text;

namespace System
{
    public static class ExtendExcption
    {
        public static string ToExString(this Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            string exMsg = $"[{ex.Message};{ex.StackTrace}]";
            sb.Append(exMsg);
            if (ex.InnerException != null)
            {
                sb.Append(ToExString(ex.InnerException));
            }
            return sb.ToString();
        }
    }
}