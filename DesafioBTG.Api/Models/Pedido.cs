using System.Text.Json.Serialization;

namespace DesafioBTG.Api.Models;

public class Pedido
{
    public int CodigoPedido  { get; set; }
    public int ClienteId { get; set; }
    [JsonIgnore]
    public Cliente? Cliente { get; set; }
    public List<Item> Itens  { get; set; } 
}