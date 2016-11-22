using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
                
           var host = new WebHostBuilder()
               .UseKestrel()
               .UseStartup<Startup>()
               .UseUrls("http://*:4000")
               .UseConfiguration(config)
               .Build();

           host.Run();
        }
    }
}
