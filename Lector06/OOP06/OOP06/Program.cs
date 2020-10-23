using System;
using System.Collections.Generic;

namespace OOP06
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(3);
            list.Add(0);
            list.Add(5);
            list.Add(4);
            list.Add(2);
            Console.WriteLine("Hello World!");
        }
    }

    class HandyMethods
    {
        public T Min<T>(List<T> l) where T:IComparable
        {
            return (T)Convert.ChangeType(0, typeof(T));
        }

        public T Max<T>(List<T> l) where T : IComparable
        {
            return (T)Convert.ChangeType(0, typeof(T));
        }
    }
}
