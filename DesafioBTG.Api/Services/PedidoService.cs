using DesafioBTG.Api.Data;
using DesafioBTG.Api.DTOs;
using DesafioBTG.Api.Models;

namespace DesafioBTG.Api.Services;

public class PedidoService
{
    private readonly DbContextApp _context;

    public PedidoService(DbContextApp context)
    {
        _context = context;
    }

    public async Task<Pedido> FazerPedido(PedidoRequest pedidoRequest, int clienteId)
    {
        var pedido = new Pedido()
        {
            ClienteId = clienteId,
            Itens = pedidoRequest.Itens
        };
        
        // _context.Context.SaveChangesAsync()
        await _context.Pedidos.AddAsync(pedido);
        await _context.SaveChangesAsync();
        
        return pedido;
    }
}