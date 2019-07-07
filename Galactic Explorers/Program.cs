using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GalacticExplorers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string envPort = Environment.GetEnvironmentVariable("PORT");
            int port = 5001;
            if(envPort != null)
            {
                port = int.Parse(envPort);
            }

            CreateWebHostBuilder(args, port).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, int port) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("https://0.0.0.0:" + port.ToString());
    }
}
