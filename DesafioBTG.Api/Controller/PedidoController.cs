using DesafioBTG.Api.Data;
using DesafioBTG.Api.DTOs;
using DesafioBTG.Api.Models;
using DesafioBTG.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBTG.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PedidosController : ControllerBase
{
    private readonly PedidoService _pedidoService;

    public PedidosController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(PedidoRequest request, int clienteId)
    {
         _pedidoService.FazerPedido(request, clienteId);
        return Ok();
    }

    [HttpGet("listar/{clienteId}")]
    public async Task<List<Pedido>> ObterPorCliente(int clienteId)
    {
        var pedidos = await _pedidoService.ListarPedidos(clienteId);
        return pedidos;
    }
    
    [HttpGet("quantidade/{clienteId}")]
    public async Task<int> ObterQuantidadePorCliente(int clienteId)
    {
        var quantidade = await _pedidoService.VerificarQuantidadePorCliente(clienteId);
        return quantidade;
    }

    [HttpGet("total/{pedidoId}")]
    public async Task<decimal> ObterValorTotalDoPedido(int pedidoId)
    {
        var valorTotal = await _pedidoService.CalcularValorTotalDoPedido(pedidoId);
        return valorTotal;
    }
}