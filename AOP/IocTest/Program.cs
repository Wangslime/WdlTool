﻿using WdlNetIoc;

namespace IocTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            IocProvider iocProvider = IocProvider.CreateIocProvider();
            iocProvider.AddScoped<Class1>();
            iocProvider.AddScoped<Class2>();
            iocProvider.AddScoped<Class3>();
            iocProvider.AddScoped<Class4>();
            iocProvider.AddScoped<Class5>();

            Class1 class1 = iocProvider.GetService<Class1>();
        }
    }


    public class Class1
    {
        public Class1(Class2 class2, Class3 class3, Class4 class4, Class5 class5)
        { 
        
        }
    }
    public class Class2 
    {
        public Class2(Class3 class3)
        {

        }
    }
    public class Class3 
    {
        public Class3(Class4 class4, Class5 class5)
        {

        }
    }
    public class Class4 
    {
        public  Class4()
        {

        }
    }
    public class Class5
    {
        public Class5(Class4 class4)
        {

        }
    }
}