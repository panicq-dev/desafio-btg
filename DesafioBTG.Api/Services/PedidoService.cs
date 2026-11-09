using DesafioBTG.Api.Data;
using DesafioBTG.Api.DTOs;
using DesafioBTG.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioBTG.Api.Services;

public class PedidoService
{
    private readonly DbContextApp _context;

    public PedidoService(DbContextApp context)
    {
        _context = context;
    }

    
    // Criando pedido.
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
    
    // Quantidade de Pedidos por Cliente
    public async Task<int> QuantidadePorCliente(int clienteId)
    {
        var clienteExiste = await _context.Clientes.AnyAsync(c => c.Id == clienteId);
        if (!clienteExiste)
        {
            return -1;
        }
        return await _context.Pedidos.CountAsync(p => p.ClienteId == clienteId);
    }
}