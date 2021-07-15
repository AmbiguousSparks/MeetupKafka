using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Order.Application.Middlewares;
using Order.Application.Settings;
using System.Text;

namespace Order.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IApplicationBuilder UseApplication(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<LogMiddleware>();
            builder.UseAuthentication();
            builder.UseAuthorization();
            return builder;
        }
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenSettings = configuration.GetSection("TokenSettings").Get<TokenSettings>();
            var key = Encoding.ASCII.GetBytes(tokenSettings.TokenSecret);
            services.AddSingleton(tokenSettings);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddLogging(config =>
            {
                config.AddFile("Logs/log-{Date}.txt", LogLevel.Error, isJson: true);
            });


            return services;
        }

        public static IServiceCollection AddCorsDefault(this IServiceCollection services)
        {
            services.AddCors(cors =>
            {
                cors.AddPolicy("DEFAULT", policy =>
                {
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                    policy.AllowAnyOrigin();
                });
            });
            return services;
        }
    }
}
