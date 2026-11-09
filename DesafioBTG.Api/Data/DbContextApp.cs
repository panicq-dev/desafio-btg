using DesafioBTG.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioBTG.Api.Data;

public class DbContextApp : DbContext
{
    public DbContextApp(DbContextOptions<DbContextApp> options) : base(options)
    {
        
    }
    
    public DbSet<Item> Itens { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    
}