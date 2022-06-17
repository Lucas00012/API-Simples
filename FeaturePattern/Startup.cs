using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using API.Core.Extensions.Configuration;

namespace API
{
    public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddApiConfiguration();

			services.AddSwaggerConfiguration();

			services.RegisterServices(Configuration);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSwaggerConfiguration();

			app.UseApiConfiguration(env);
		}
	}
}
