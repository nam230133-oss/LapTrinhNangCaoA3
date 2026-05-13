// File: QuanLyPhongGym.API/Controllers/HoiVienController.cs

using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongGym.Application.Features.HoiVien.Commands.Create;
using QuanLyPhongGym.Application.Features.HoiVien.Commands.Delete;
using QuanLyPhongGym.Application.Features.HoiVien.Queries.GetList;

namespace QuanLyPhongGym.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HoiVienController : ControllerBase
{
    private readonly IMediator _mediator;

    // Tiêm (Inject) IMediator vào Controller
    public HoiVienController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHoiVienCommand command)
    {
        // Mediator sẽ tự động tìm CreateHoiVienCommandHandler ở tầng Application để chạy
        var newId = await _mediator.Send(command);

        return Ok(new
        {
            message = "Thêm hội viên thành công",
            id = newId
        });
    }
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var query = new GetHoiVienListQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteHoiVienCommand(id);
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound(new { message = "Không tìm thấy hội viên để xóa" });
        }

        return Ok(new { message = "Xóa hội viên thành công" });
    }
}