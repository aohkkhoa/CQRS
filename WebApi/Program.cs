using Application.Features.BookFeatures.Commands;
using Application.Features.BookFeatures.Queries;
using Application.Features.CategoryFeatures.Queries;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var assembly = AppDomain.CurrentDomain.GetAssemblies();
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("MyDB")));
/*builder.Services.AddMediatR(typeof(GetAllBookQuery).Assembly,
                            typeof(GetAllCategoryQuery).Assembly,
                            typeof(CreateBookCommand).Assembly);*/
builder.Services.AddMediatR(typeof(IBookRepository).Assembly,
                            typeof(IOrderRepository).Assembly,
                            typeof(IOrderDetailRepository).Assembly,
                            typeof(IStorageRepository).Assembly,
                            typeof(ICategoryRepository).Assembly);
builder.Services.AddScoped<IStorageRepository, StorageRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IMediator, Mediator>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
