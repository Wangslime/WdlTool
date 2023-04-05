using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ExtendPublic
{
    public static class ExtendEnity
    {
        public static T DeepCopy<T>(this T obj) where T : class , new()
        {
            T t;
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, obj);
            memoryStream.Position = 0;
            t = (T)formatter.Deserialize(memoryStream);
            return t;
        }
    }
}