using System;

namespace Watchdog.ProxyListener
{
    class Program
    {
        static void Main(string[] args)
        {
            new ContainerBuilder()
                .WithStartup<Startup>()
                .Build();

            Console.ReadLine();
        }
    }
}
