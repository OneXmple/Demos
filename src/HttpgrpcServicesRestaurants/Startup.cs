using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using httpgrpc.common.Commands;
using httpgrpc.services.restaurants.Integration_Handlers;
using Httpgrpc.Common.Commands;
using Httpgrpc.Common.RabbitMq;
using Httpgrpc.Services.Restaurants.Domain.Models;
using Httpgrpc.Services.Restaurants.Repositories;
using Httpgrpc.Services.Restaurants.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Httpgrpc.Services.Restaurants
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
            services.AddControllers();
            services.AddRabbitMq(Configuration);
            services.AddTransient<ICommandHandler<CreateRestaurant>, CreateRestaurantHandler>();
            services.Configure<RestaurantSettings>(Configuration);
            services.AddTransient<IRestaurantRepository, RestaurantRepository>();
            services.AddTransient<IRestaurantService, RestaurantService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            RestaurantContextSeed.SeedAsync(app).Wait();
        }
    }
}
