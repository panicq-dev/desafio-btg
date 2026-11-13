using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DesafioBTG.Worker;
 
 public class Worker : BackgroundService
 {
     private const string nomeFila = "q.pedidos";
     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
     {

         var factory = new ConnectionFactory
         {
             HostName = "localhost",
             UserName = "guest",
             Password = "guest"
         };
         
         await using var connection =
             await factory.CreateConnectionAsync();

         await using var channel =
             await connection.CreateChannelAsync();
         
         // Queue
         await channel.QueueDeclareAsync(
             queue: nomeFila,
             durable: true,
             autoDelete: false,
             exclusive: false,
            cancellationToken: stoppingToken
         );

         var consumer = new AsyncEventingBasicConsumer(channel);
         
         consumer.ReceivedAsync += async (sender, eventArgs) =>
         {
             byte[] body = eventArgs.Body.ToArray();

             string mensagem = Encoding.UTF8.GetString(body);

             Console.WriteLine($"Mensagem recebida: {mensagem}");

             await Task.CompletedTask;
         };
         
         await channel.BasicConsumeAsync(
             queue: nomeFila,
             autoAck: true,
             consumer: consumer,
                cancellationToken: stoppingToken
         );
         await Task.Delay(Timeout.Infinite, stoppingToken);
     }
     
 }