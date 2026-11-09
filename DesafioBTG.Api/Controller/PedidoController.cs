using DesafioBTG.Api.Data;
using DesafioBTG.Api.DTOs;
using DesafioBTG.Api.Models;
using DesafioBTG.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBTG.Api.Controller;

[ApiController]
[Route("[controller]")]
public class PedidoController : ControllerBase
{
    private readonly PedidoService _pedidoService;

    public PedidoController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost]
    public async Task<IActionResult> RealizarPedido(PedidoRequest pedidoRequest, int clienteId)
    {
        var resposta = _pedidoService.FazerPedido(pedidoRequest, clienteId);
        return Ok(resposta);
    }
}