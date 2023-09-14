using FluentValidation;
using FluentValidation.AspNetCore;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Contracts.Validation;
using HomeApi.DAL;
using HomeApi.Mapping;
using HomeApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("HomeOptions.json");

// Добавляем контекст БД
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")));

// Добавляем маппинг
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Добавляем новый сервис
builder.Services.Configure<HomeOptions>(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HomeApi",
        Version = "v1"
    });
});

// Подключаем класс валидации
builder.Services.AddFluentValidationAutoValidation(); 
builder.Services.AddFluentValidationClientsideAdapters(); 
builder.Services.AddValidatorsFromAssembly(typeof(AddDeviceRequestValidator).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
