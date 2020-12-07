using System;
using System.Threading;
using Argutec.Profiling;

namespace Example.Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new Profiler("MyMethod"))
            {
                Thread.Sleep(1000);
            }
        }
    }
}
