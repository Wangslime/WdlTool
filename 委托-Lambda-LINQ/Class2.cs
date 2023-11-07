using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 委托_Lambda_LINQ;

namespace 委托_Lambda_LINQ1
{
    internal class Class2
    {
        public DDD ddd = new();
        public string a = "0";
        public List<int> d = null;



        public static int add()
        {
            return 1 + 1;
        }
    }

    public enum BBB
    { 
        a,
        b,
        c,
        d,
        e,
        f,
        g,
    }

    public struct DDD
    { 
        public int a;
    }
}
