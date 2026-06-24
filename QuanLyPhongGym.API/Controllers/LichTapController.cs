using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongGym.Application.Features.LichTaps.Commands.CreateLichTap;
using QuanLyPhongGym.Application.Features.LichTaps.Queries.GetLichTapByHoiVien;
using QuanLyPhongGym.Application.Features.LichTaps.Queries.GetLichTapByPt;

[ApiController]
[Route("api/[controller]")]
public class LichTapController : ControllerBase
{
    private readonly IMediator _mediator;
    public LichTapController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLichTapCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpGet("HoiVien/{hoiVienId}")]
    public async Task<IActionResult> GetByHoiVien(Guid hoiVienId)
    {
        return Ok(await _mediator.Send(new GetLichTapByHoiVienQuery(hoiVienId)));
    }

    [HttpGet("Pt/{ptId}")]
    public async Task<IActionResult> GetByPt(Guid ptId)
    {
        return Ok(await _mediator.Send(new GetLichTapByPtQuery(ptId)));
    }
}