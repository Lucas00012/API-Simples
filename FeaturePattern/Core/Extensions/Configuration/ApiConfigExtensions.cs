using API.Core.Helpers.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API.Core.Extensions.Configuration
{
    public static class ApiConfigExtensions
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
			services.AddControllers(opt =>
			{
				opt.Filters.Add(typeof(DbContextTransactionFilter));
				opt.Filters.Add(typeof(ExceptionFilter));
			})
			.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Startup>());

			services.AddApiVersioning(opt =>
			{
				opt.ReportApiVersions = true;
				opt.AssumeDefaultVersionWhenUnspecified = true;
				opt.DefaultApiVersion = new ApiVersion(1, 0);
			});

			services.AddVersionedApiExplorer(opt =>
			{
				opt.GroupNameFormat = "'v'VVV";
				opt.SubstituteApiVersionInUrl = true;
			});

			return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseCors();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			return app;
        }
    }
}
