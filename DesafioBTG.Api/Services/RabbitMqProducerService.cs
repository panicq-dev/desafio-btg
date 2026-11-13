using System.Text;
using System.Text.Json;
using DesafioBTG.Api.Models;
using RabbitMQ.Client;
namespace DesafioBTG.Api.Services;

public class RabbitMqProducerService
{
    
    private const string nomeFila = "q.pedidos";
    private const string nomeExchange = "ex.pedidos";
    private const string routingKey = "pedidos";
    
    public async Task PublicarPedidoAsync(Pedido mensagem)
    {
        
        // Criando conexão.
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        
        var jsonMsg = JsonSerializer.Serialize(mensagem);
        byte[] body = Encoding.UTF8.GetBytes(jsonMsg);

        
        // Publish
        await channel.BasicPublishAsync(
            exchange: nomeExchange,
            routingKey: routingKey,
            body: body
            );
        
        // Exchange
        await channel.ExchangeDeclareAsync(
            exchange: nomeExchange, 
            type: ExchangeType.Direct
            );
        
        // Binding
        await channel.QueueBindAsync(
            exchange: nomeExchange,
            routingKey: routingKey,
            queue: nomeFila);
        
        // Fila
        await channel.QueueDeclareAsync(
            queue: nomeFila,
            durable: true,
            autoDelete: false,
            exclusive: false
        );
    }
    
    
    
}