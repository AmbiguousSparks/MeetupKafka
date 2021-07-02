using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Order.Consumer.Hubs;
using Order.Consumer.Services;
using Order.Domain.Configurations;
using System;

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
            services.AddControllersWithViews();
            KafkaConfig config = new()
            {
                BootstrapServer = Environment.GetEnvironmentVariable("KAFKA_HOST"),
                Topics = new System.Collections.Generic.List<string>()
                {
                    Environment.GetEnvironmentVariable("TOPIC_NEW_ORDER")
                }
            };
            services.AddSingleton(config);
            services.AddHostedService<OrderConsumer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "Default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<InvoiceHub>("/invoice-hub");
            });
        }
    }
}
