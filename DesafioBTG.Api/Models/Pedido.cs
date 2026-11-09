namespace DesafioBTG.Api.Models;

public class Pedido
{
    public Guid CodigoPedido  { get; set; }
    public int ClienteId { get; set; }
    public List<Item> Itens  { get; set; } 
}