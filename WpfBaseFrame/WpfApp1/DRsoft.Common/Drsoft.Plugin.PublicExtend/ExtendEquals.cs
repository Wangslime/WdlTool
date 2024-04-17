using System.Reflection;

namespace Drsoft.Plugin.PublicExtend
{
    public static class ExtendEquals
    {
        //public static bool DynamicEquals(this object obj, dynamic obj2)
        //{
        //    dynamic obj1 = obj;
        //    if (obj1 == null && obj2 == null)
        //    {
        //        return true;
        //    }
        //    else if (obj1 == null || obj2 == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            Type type1 = obj1.GetType();
        //            Type type2 = obj2.GetType();
        //            if (type1 != type2)
        //            {
        //                return false;
        //            }
        //            PropertyInfo[] propertyInfos1 = type1.GetProperties();
        //            if (propertyInfos1 != null && propertyInfos1.Any())
        //            {
        //                foreach (PropertyInfo propertyInfo in propertyInfos1)
        //                {
        //                    dynamic value1 = propertyInfo.GetValue(obj1);
        //                    dynamic value2 = propertyInfo.GetValue(obj2);
        //                    if (propertyInfo.PropertyType.IsArray)
        //                    {
        //                        for (int i = 0; i < value1.Length; i++)
        //                        {
        //                            bool ret = DynamicEquals(value1[i], value2[i]);
        //                            if (!ret)
        //                            {
        //                                return false;
        //                            }
        //                        }
        //                    }
        //                    else if (propertyInfo.PropertyType == typeof(string))
        //                    {
        //                        bool ret = value1 == value2;
        //                        if (!ret)
        //                        {
        //                            return false;
        //                        }
        //                    }
        //                    else if (propertyInfo.PropertyType.IsValueType)
        //                    {
        //                        bool ret = value1 == value2;
        //                        if (!ret)
        //                        {
        //                            return false;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        bool ret = DynamicEquals(value1, value1);
        //                        if (!ret)
        //                        {
        //                            return false;
        //                        }
        //                    }
        //                }
        //                return true;
        //            }
        //            else
        //            {
        //                Type type11 = obj1.GetType();
        //                if (type11.IsArray)
        //                {
        //                    for (int i = 0; i < obj1.Length; i++)
        //                    {
        //                        bool ret = DynamicEquals(obj1[i], obj2[2]);
        //                        if (!ret)
        //                        {
        //                            return false;
        //                        }
        //                    }
        //                }
        //                else if (type11 is string)
        //                {
        //                    bool ret = obj1 == obj2;
        //                    if (!ret)
        //                    {
        //                        return false;
        //                    }
        //                }
        //                else if (type11.IsValueType)
        //                {
        //                    bool ret = obj1 == obj2;
        //                    if (!ret)
        //                    {
        //                        return false;
        //                    }
        //                }
        //                else
        //                {
        //                    bool ret = DynamicEquals(obj1, obj2);
        //                    if (!ret)
        //                    {
        //                        return false;
        //                    }
        //                }
        //                return true;
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //    }
        //}
    }
}
