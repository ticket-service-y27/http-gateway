using HttpGateway.Clients;
using HttpGateway.Models.Tickets.Promocodes;
using Microsoft.AspNetCore.Mvc;
using TicketService.Grpc.Promocodes;

namespace HttpGateway.Controllers;

[ApiController]
[Route("gateway/promocodes")]
public class PromocodesController : ControllerBase
{
    private readonly IPromocodeServiceGrpcClient _client;

    public PromocodesController(IPromocodeServiceGrpcClient client)
    {
        _client = client;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePromocodeDto dto, CancellationToken ct)
    {
        await _client.CreatePromocodeAsync(dto.Code, dto.DiscountPercentage, dto.Count, ct);
        return Ok();
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<GetPromocodeDto>> GetByCode([FromRoute] string code, CancellationToken ct)
    {
        Promocode p = await _client.GetPromocodeAsync(code, ct);

        return Ok(new GetPromocodeDto(p.Id, p.Code, p.DiscountPercentage, p.Count));
    }
}