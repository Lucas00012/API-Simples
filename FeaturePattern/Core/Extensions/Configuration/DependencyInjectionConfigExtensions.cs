using API.Database;
using API.Database.Repository;
using API.Database.Repository.Contracts;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.Core.Extensions.Configuration
{
    public static class DependencyInjectionConfigExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(typeof(Startup));

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));

            return services;
        }
    }
}
