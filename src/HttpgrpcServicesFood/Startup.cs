using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Httpgrpc.Common.Commands;
using Httpgrpc.Common.RabbitMq;
using Httpgrpc.Services.Foods.Domain.Models;
using Httpgrpc.Services.Foods.Repositories;
using Httpgrpc.Services.Foods.Services;
using Httpgrpc.Common.Auth;
using httpgrpc.services.food.Grpc;

namespace Httpgrpc.Services.Food
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
            });

            services.AddControllers();
            //services.AddLogging(logging =>
            //{
            //    // clear default logging providers
            //    logging.ClearProviders();

            //    // add built-in providers manually, as needed 
            //    logging.AddConsole();
            //    logging.AddDebug();
            //    logging.AddEventLog();
            //    logging.AddEventSourceLogger();
            //});
            services.AddLogging();
            services.AddJwt(Configuration);
            services.AddRabbitMq(Configuration);
            services.Configure<FoodSettings>(Configuration);
            services.AddTransient<IFoodRepository, FoodRepository>();
            services.AddTransient<IFoodService, FoodService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcFoodService>();
                endpoints.MapControllers();
            });
        }
    }
}
