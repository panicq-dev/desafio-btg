namespace DesafioBTG.Api.Models;

public class Item
{
    public string Produto { get; set; }
    public int  Quantidade { get; set; }
    public decimal Preco { get; set; }
    public Guid PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;
}