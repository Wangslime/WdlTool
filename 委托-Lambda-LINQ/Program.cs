using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace 委托_Lambda_LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Class1.a = 12;

            Class1 class1 = new Class1();
            class1.b = 13;



            



            List<int> list = new List<int>() {1,2,3,4,5,6,7 };

            #region Linq函数
            ////根据指定条件筛选元素
            //list.Where();

            ////筛选指定类型的元素
            //list.OfType();

            ////获取满足条件的第一个元素
            //list.First();
            //list.FirstOrDefault();

            ////获取满足条件的最后一个元素
            //list.Last();
            //list.LastOrDefault();

            ////获取满足条件的唯一一个元素
            //list.Single();
            //list.SingleOrDefault();

            ////返回指定数量的元素
            //list.Take();

            ////获取满足条件的元素，直到条件不再满足
            //list.TakeWhile();

            ////跳过指定数量的元素
            //list.Skip();

            ////跳过满足条件的元素，直到条件不再满足
            //list.SkipWhile();

            ////去除序列中的重复元素
            //list.Distinct();

            ////升序/降序/然后再排序
            //list.OrderBy()
            //    .ThenBy();
            //list.OrderByDescending()
            //    .ThenByDescending();

            ////反转元素的顺序
            //list.Reverse();

            ////在集合为空时返回默认值
            //list.DefaultIfEmpty();

            ////对每个元素进行转换，返回新的序列
            //list.Select();

            ////将每个元素的集合合并成单个序列
            //list.SelectMany();

            ////将一个序列强制转换为指定类型的序列
            //list.Cast();

            ////将一个序列强制转换为指定类型的序列
            //list.ToArray();

            ////将序列转换为列表
            //list.ToList();

            ////将序列转换为字典
            //list.ToDictionary();

            ////将序列分组为Lookup，类似于字典，但一个键可以对应多个值
            //list.ToLookup();

            ////将序列转换为IEnumerable类型，可以在LINQ查询中进行延迟求值,常用将Datatable数据转换成IEnumerable类型，从而进行各种linq操作
            //list.AsEnumerable();


            ////判断序列中是否存在满足指定条件的元素
            //list.Any();

            ////判断序列中是否包含指定元素
            //list.Contains();

            ////统计序列中元素的个数
            //list.Count();

            ////计算序列中元素的和
            //list.Sum();

            ////计算序列中元素的平均值
            //list.Average();

            ////获取序列中的最小值
            //list.Min();

            ////获取序列中的最大值
            //list.Max();

            ////对序列中的元素进行累积计算
            //list.Aggregate();

            ////判断序列的所有元素是否满足指定条件
            //list.All();



            ////将两个序列合并成一个序列
            //list.Concat();

            ////获取两个序列的并集，去除重复元素
            //list.Union();

            ////获取两个序列的交集
            //list.Intersect();

            ////获取第一个序列中不包含在第二个序列中的元素
            //list.Except();

            ////将两个序列的对应元素进行配对
            //list.Zip();

            ////判断两个序列是否相等
            //list.SequenceEqual();

            ////按照指定的键对序列进行分组
            //list.GroupBy();

            ////将序列分组为Lookup，类似于字典，但一个键可以对应多个值
            //list.ToLookup();

            ////按照指定的键将两个序列进行分组并进行连接
            //list.GroupJoin();

            ////按照指定的键将两个序列进行连接
            //list.Join();

            ////在进行分组操作后，将每个组内的元素展开为一个平铺的序列
            //list.SelectMany();



            #endregion


            // 常规的Linq语句，实际上Where的参数就是一个Func委托
            list = list.Where(p=> p < 5).ToList();

            //将这个参数的Func委托进行还原
            Func<int, bool> func = p => p < 5;
            list = list.Where(func).ToList();

            //继续还原
            Func<int, bool> func1 = (p) => { return p < 5; };
            list = list.Where(func1).ToList();

            //继续还原
            Func<int, bool> func2= delegate (int p) { return p < 5; };
            list = list.Where(func2).ToList();

            //继续还原
            Func<int, bool> func3 = Select;

            //自己写的Linq函数使用
            var whereList = list.DlWhere(p => p == 2).ToList();
            bool selectBool = list.DlSelect(p=> p == 2).First();
            bool anyBool = list.DlAny(p => p == 2);
        }

        public static bool Select(int p)
        {
            Class1 class2 = new Class1();
            class2.b = 18;

            return p < 5;
        }
    }


    public static class ExtendLinq
    {
        public static IEnumerable<T?> DlWhere<T>(this IEnumerable<T>? list, Func<T,bool> func)
        {
            if (list == null)
            {
                yield return default;
            }
            else
            {
                foreach (var item in list)
                {
                    if (func.Invoke(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IEnumerable<T2?> DlSelect<T1,T2>(this IEnumerable<T1> list, Func<T1, T2> func)
        {
            if (list == null)
            {
                yield return default;
            }
            else
            {
                foreach (var item in list)
                {
                    yield return func.Invoke(item);
                }
            }
        }

        public static bool DlAny<T>(this IEnumerable<T> list, Func<T, bool> func)
        {
            if (list == null)
            {
                return false;
            }
            foreach (var item in list)
            {
                if (func.Invoke(item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}