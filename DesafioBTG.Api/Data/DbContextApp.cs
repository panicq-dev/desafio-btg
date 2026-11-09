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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Cliente>(entidade =>
        {
            entidade.HasKey(c => c.Id);

            entidade.HasMany(c => c.Pedidos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Pedido>(entidade =>
        {
            entidade.HasKey(p => p.CodigoPedido);
            
            entidade.HasMany(p => p.Itens)
                .WithOne(i => i.Pedido)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Item>(entidade =>
        {
            entidade.HasKey(c => c.Id);
        });

    }
    
}