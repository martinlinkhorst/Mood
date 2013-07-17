using System;
using System.Diagnostics;
using Nancy.Hosting.Self;

namespace MoodApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var nancyHost = new NancyHost(new Uri("http://localhost:9000/"), new Uri("http://127.0.0.1:9000/"), new Uri("http://localhost:9000/"));
            nancyHost.Start();
 
            Console.WriteLine("Nancy now listening - navigating to http://localhost:9000/. Press enter to stop");
            Process.Start("http://localhost:9000/");
            Console.ReadKey();
 
            nancyHost.Stop();
 
            Console.WriteLine("Stopped. Good bye!");
        }
    }
}
