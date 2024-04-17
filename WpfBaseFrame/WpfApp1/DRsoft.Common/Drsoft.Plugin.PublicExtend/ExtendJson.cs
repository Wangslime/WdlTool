using Newtonsoft.Json;

namespace Drsoft.Plugin.PublicExtend
{
    public static class ExtendJson
    {
        public static string ToJson(this object obj, bool Indented = false)
        {
            try
            {
                if (Indented)
                {
                    return JsonConvert.SerializeObject(obj, Formatting.Indented);
                }
                else
                {
                    return JsonConvert.SerializeObject(obj);
                }
            }
            catch (Exception ex)
            {
                return "";// ex.ToExString();
            }
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

        public static T? ReadJsonFile<T>(this string filePath)
        {
            if (!File.Exists(filePath))
            {
                return default;
            }
            string strConfig = File.ReadAllText(filePath);
            T t = strConfig.JsonToObj<T>();
            return t;
        }

        public static void SaveJsonFile<T>(this T t, string filePath)
        {
            var dic = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dic))
            {
                Directory.CreateDirectory(dic);
            }
            string strData = t.ToJson(true);
            File.WriteAllText(filePath, strData);
        }
    }
}
