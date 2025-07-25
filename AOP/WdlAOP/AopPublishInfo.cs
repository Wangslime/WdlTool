﻿using System;
using System.Reflection;

namespace Long.ProxyAOP
{
    public class AopPublishInfo
    {
        public string Name { get; set; }
        public dynamic MainType { get; set; }
        public MethodInfo Method { get; set; }
        public dynamic Result { get; set; }
        public double Time { get; set; }
        public object[] ParObjs { get; set; }
        public Exception Ex { get; set; }
    }
}
