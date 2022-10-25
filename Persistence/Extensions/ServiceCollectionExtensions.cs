using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.repositories;
using Persistence.Repositories;
using Persistence.Repository;
using Persistence.SeedingData;
using FluentValidation.AspNetCore;

using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;
using Application.Features.BookFeatures.Commands.Create;
using FluentValidation;

namespace Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var builder = WebApplication.CreateBuilder();
            ConfigurationManager configuration = builder.Configuration;

            services.AddMediatR(typeof(IBookRepository).Assembly,
                typeof(IOrderRepository).Assembly,
                typeof(IOrderDetailRepository).Assembly,
                typeof(IStorageRepository).Assembly,
                typeof(ICategoryRepository).Assembly,
                typeof(IPermissionRepository).Assembly,
                typeof(ITestRepository).Assembly
            );
            services.AddScoped<IStorageRepository, StorageRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("MyDB")));
        }

        public static void AddFluentValidator(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(typeof(CreateBookCommandValidator).Assembly);
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
    }
}