using Application.Features.BookFeatures.Commands.Create;
using Application.Validators.Features.Books.Commands;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Persistence.Extensions;
using System;

var builder = WebApplication.CreateBuilder(args);
var assembly = AppDomain.CurrentDomain.GetAssemblies();
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// add mediatr and repository
builder.Services.AddApplication();
// add DBcontext
builder.Services.AddDBContext(configuration);
builder.Services.AddScoped<IMediator, Mediator>();


builder.Services.AddScoped<IValidator<CreateBookCommand>, CreateBookCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookCommandValidator>();
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
