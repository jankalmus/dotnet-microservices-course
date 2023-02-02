using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Data.Contracts;
using PlatformService.Data.Repository;
using PlatformService.DataServices.Asynchronous.MessageBus;
using PlatformService.DataServices.Synchronous.Http;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>(); 

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();  

builder.Services.AddDbContext<AppDbContext>(options =>
{
    Console.WriteLine($"INFO: Database connectionString: {builder.Configuration.GetConnectionString("PlatformsDatabase")}");
    options.UseNpgsql(builder.Configuration.GetConnectionString("PlatformsDatabase"));
});

Console.WriteLine($"INFO: CommandService endpoint: {builder.Configuration["CommandService"]}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.SeedDatabase();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();