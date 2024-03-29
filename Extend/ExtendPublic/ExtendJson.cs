﻿using Newtonsoft.Json;

namespace ExtendPublic
{
    public static class ExtendJson
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T JsonToObj<T>(this string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str)) { return default; }
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public static T DeepCopy<T>(this T t)
        {
            return t.ToJson().JsonToObj<T>();
        }
    }
}
