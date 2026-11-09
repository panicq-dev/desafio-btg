using System.Text.Json.Serialization;

namespace DesafioBTG.Api.Models;

public class Item
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }
    [JsonIgnore]
    public int PedidoId { get; set; }
    [JsonIgnore]
    public Pedido Pedido { get; set; } = null!;
}