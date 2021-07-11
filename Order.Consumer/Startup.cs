using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Order.Consumer.Extensions;
using Order.Consumer.Hubs;
using MediatR;
using Order.Consumer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Order.Application.Extensions;
using Order.Application.Handlers;

namespace Order.Consumer
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
            services.AddMvcCore();
            services.AddSignalR();
            services.AddCors(cors =>
            {
                cors.AddPolicy("DEFAULT", policy =>
                {
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                    policy.AllowAnyOrigin();
                });
            });
            services.AddControllersWithViews();

            services.AddLogging(config =>
            {
                config.AddFile("Logs/log-{Date}.txt", LogLevel.Information, isJson: true);
            });

            services.RegistryMongoService(Configuration.GetConnectionString("Mongo"));
            services.AddProducers(Configuration);
            services.AddConsumers(Configuration);
            var assembly = AppDomain.CurrentDomain.Load(typeof(InvoiceHandler).GetTypeInfo().Assembly.FullName);
            services.AddMediatR(assembly);
            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "ClientApp/dist";
            });
            //TODO: refatorar código do startup
            services.AddHostedService<OrderListenerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseApplication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "Default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<InvoiceHub>("/invoice-hub");
            });

            app.UseSpa(spa =>
            {
                spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
