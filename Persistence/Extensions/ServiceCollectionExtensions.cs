using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddDBContext(this IServiceCollection services, IConfiguration configuration)
        {
          services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("MyDB")));

        }
        public static void AddApplication(this IServiceCollection services)
        {
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
                            typeof(ICategoryRepository).Assembly);
            services.AddScoped<IStorageRepository, StorageRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}
