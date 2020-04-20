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
using Httpgrpc.Services.Identity.Domain.Models;
using Httpgrpc.Services.Identity.Services;
using Httpgrpc.Services.Identity.Repositories;
using Httpgrpc.Common.RabbitMq;
using Httpgrpc.Common.Auth;
using Httpgrpc.Services.Identity.Domain.Services;
using Httpgrpc.Common.Commands;
using Httpgrpc.Services.Identity.Handlers;
using Microsoft.AspNetCore.Http;

namespace Httpgrpc.Services.Identity
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
            services.AddLogging();
            services.AddJwt(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddTransient<ICommandHandler<CreateUser>, CreateUserHandler>();
            services.Configure<IdentitySettings>(Configuration);
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<IEncrypter, Encrypter>();

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

            UsersContextSeed.SeedAsync(app).Wait();
        }
    }
}
