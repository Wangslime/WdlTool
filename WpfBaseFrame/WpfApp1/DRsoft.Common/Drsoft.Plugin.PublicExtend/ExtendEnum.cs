﻿namespace Drsoft.Plugin.PublicExtend
{
    public static class ExtendEnum
    {
        public static T ToEnum<T>(this string str)
        {
            return (T)Enum.Parse(typeof(T), str);
        }
    }
}
