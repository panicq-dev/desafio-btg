namespace DesafioBTG.Api.Models;

public class Cliente
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public List<Pedido> Pedidos { get; set; }
    
    
}