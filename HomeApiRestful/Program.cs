using FluentValidation;
using FluentValidation.AspNetCore;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Contracts.Validation;
using HomeApi.DAL;
using HomeApi.DAL.Repositories.Interfaces;
using HomeApi.DAL.Repositories;
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

// ��������� �������� ��
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")));

// ��������� �������
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// ����������� ������� ����������� ��� �������������� � ��
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

// ��������� ����� ������
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

// ���������� ������ ���������
builder.Services.AddFluentValidationAutoValidation(); 
builder.Services.AddFluentValidationClientsideAdapters(); 
builder.Services.AddValidatorsFromAssembly(typeof(AddDeviceRequestValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(AddRoomRequestValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(EditDeviceRequestValidator).Assembly);

// ������ DateTime � timestamp ��� ���������� ������ � ��
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
