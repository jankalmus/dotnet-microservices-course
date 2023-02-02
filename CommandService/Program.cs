using AutoMapper;
using CommandService.Data;
using CommandService.Data.Contracts;
using CommandService.Data.Repository;
using CommandService.DataServices;
using CommandService.DataServices.gRPC;
using CommandService.Events.Processing;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICommandRepository, CommandRepository>(); 
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddScoped<IPlatformDataClient, PlatformDataClient>(); 

builder.Services.AddHostedService<MessageBusSubscriber>(); 

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMem")); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.PopulatePlatforms();

app.Run();