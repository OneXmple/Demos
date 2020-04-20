using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using httpgrpc.common.Commands;
using Httpgrpc.Common.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Httpgrpc.Services.Restaurants
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
               .UseRabbitMq()
               .SubscribeToCommand<CreateRestaurant>()
               .Build()
               .Run();

            //CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
