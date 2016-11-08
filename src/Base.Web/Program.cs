using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Base.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
             var host = new WebHostBuilder()
                .UseKestrel(o=> {
                    o.AddServerHeader = false;
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
