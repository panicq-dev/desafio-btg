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
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        // 1. Exchange
        await channel.ExchangeDeclareAsync(
            exchange: nomeExchange,
            type: ExchangeType.Direct
        );

        // 2. Queue
        await channel.QueueDeclareAsync(
            queue: nomeFila,
            durable: true,
            autoDelete: false,
            exclusive: false,
            arguments: null
        );

        // 3. Binding
        await channel.QueueBindAsync(
            queue: nomeFila,
            exchange: nomeExchange,
            routingKey: routingKey
        );

        // 4. Publish (sempre por último)
        var jsonMsg = JsonSerializer.Serialize(mensagem);
        byte[] body = Encoding.UTF8.GetBytes(jsonMsg);

        await channel.BasicPublishAsync(
            exchange: nomeExchange,
            routingKey: routingKey,
            body: body
        );
    }
}