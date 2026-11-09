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
    public async Task<int> VerificarQuantidadePorCliente(int clienteId)
    {
        var clienteExiste = await _context.Clientes.AnyAsync(c => c.Id == clienteId);
        if (!clienteExiste)
        {
            return -1;
        }
        return await _context.Pedidos.CountAsync(p => p.ClienteId == clienteId);
    }
    
    // Valor total do pedido.
    public async Task<decimal> CalcularValorTotalDoPedido(int pedidoId)
    {
        
        var pedidoExiste = await _context.Pedidos.AnyAsync(p => p.CodigoPedido == pedidoId);
        if (!pedidoExiste) return -1m;
        
        var totalPedido = await _context.Pedidos
            .Where(p => p.CodigoPedido == pedidoId)
            .SelectMany(p => p.Itens)
            .SumAsync(item => item.Preco);

        return totalPedido;
    }
    
    // Listagem de pedidos.
    public async Task<List<Pedido>> ListarPedidos(int clienteId)
    {
        var clienteExistente = await _context.Clientes.AnyAsync(c => c.Id == clienteId);
        if (!clienteExistente) return null;
        
        var listaPedidos = await _context.Clientes.Where(c => c.Id == clienteId)
            .SelectMany(p => p.Pedidos)
            .ToListAsync();

        return listaPedidos;
    }

}