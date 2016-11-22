using System;
using Microsoft.AspNetCore.Hosting;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host = new WebHostBuilder()
               .UseKestrel()
               .UseStartup<Startup>()
               .UseUrls("http://*:4000")
               .Build();

           host.Run();
        }
    }
}
