using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using mysqlefcore;
using PikyServer.Models;

namespace PikyServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            Context();
        }
        private static void Context() {
            using(var context = new PikyContext())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();

                // Saves changes
                context.SaveChanges();
            }
            
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
