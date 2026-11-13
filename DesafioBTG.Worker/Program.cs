using DesafioBTG.Api.Data;      
using DesafioBTG.Api.Services;  
using DesafioBTG.Worker;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<DbContextApp>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<PedidoService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();