using System;

namespace Sample.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            new Bootstrapper().Bootstrap();

            Console.ReadLine();
        }
    }
}
