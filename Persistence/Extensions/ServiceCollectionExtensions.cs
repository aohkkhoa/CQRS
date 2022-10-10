using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using Persistence.repositories;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.SeedingData;
using WebApi;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

namespace Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("MyDB")));

        }
        public static IApplicationBuilder InitializeDb(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var initializers = serviceScope.ServiceProvider.GetServices<IDatabaseSeeder>();

            foreach (var initializer in initializers)
            {
                initializer.Initialize();
            }

            return app;
        }
        public static void AddApplication(this IServiceCollection services)
        {
            var builder = WebApplication.CreateBuilder();
            ConfigurationManager configuration = builder.Configuration;
            /*builder.Services.AddMediatR(typeof(GetAllBookQuery).Assembly,
                            typeof(GetAllCategoryQuery).Assembly,
                            typeof(CreateBookCommand).Assembly);*/


            /*builder.Services.AddMediatR(typeof(IBookRepository).Assembly,
                                        typeof(IOrderRepository).Assembly,
                                        typeof(IOrderDetailRepository).Assembly,
                                        typeof(IStorageRepository).Assembly,
                                        typeof(ICategoryRepository).Assembly);
            builder.Services.AddScoped<IStorageRepository, StorageRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();*/
            services.AddMediatR(typeof(IBookRepository).Assembly,
                            typeof(IOrderRepository).Assembly,
                            typeof(IOrderDetailRepository).Assembly,
                            typeof(IStorageRepository).Assembly,
                            typeof(ICategoryRepository).Assembly,
                            typeof(IAuthRepository).Assembly
                            );
            services.AddScoped<IStorageRepository, StorageRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();


            /* services.AddSession(options =>
             {
                 options.IdleTimeout = TimeSpan.FromMinutes(20);
             });
             services.Configure<ApplicationSettings>(
                 configuration.GetSection("ApplicationSettings"));
             services.AddAuthentication(x =>
             {
                 x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             }).AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ApplicationSettings:Secret"])),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });*/
        }
    }
}
