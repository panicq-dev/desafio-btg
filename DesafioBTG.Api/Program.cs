using DesafioBTG.Api.Data;
using DesafioBTG.Api.Services;
using Microsoft.EntityFrameworkCore;
// Variáveis locais.
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Services.
builder.Services.AddOpenApi();
builder.Services.AddDbContext<DbContextApp>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<PedidoService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<RabbitMqProducerService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioBTG API v1"));
}
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
