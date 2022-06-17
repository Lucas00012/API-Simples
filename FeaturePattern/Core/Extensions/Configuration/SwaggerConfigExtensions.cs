using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace API.Core.Extensions.Configuration
{
    public static class SwaggerConfigExtensions
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "Versão 1.0",
                    Description = "API p/ testes"
                });

                opt.CustomSchemaIds(type => type.FullName.Split('.').Last().Replace("+", string.Empty));
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "API - v1");
                opt.RoutePrefix = "docs";
            });

            return app;
        }
    }
}
