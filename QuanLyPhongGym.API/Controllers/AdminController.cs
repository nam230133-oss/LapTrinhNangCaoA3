
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Application.Features.Admin.Commands.CreatePtAccount;
using QuanLyPhongGym.Application.Features.Admin.Commands.CreateStaffAccount;
using QuanLyPhongGym.Application.Features.Admin.Commands.DeleteStaffOrPt;
using QuanLyPhongGym.Application.Features.Admin.Queries;
using QuanLyPhongGym.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IApplicationDbContext _context;
    // 1. Khai báo biến _mediator
    private readonly IMediator _mediator;

    // 2. Cập nhật Constructor để nhận IMediator (Dependency Injection)
    public AdminController(IApplicationDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    // 3. Tạo tài khoản cho PT
    [HttpPost("create-pt")]
    public async Task<IActionResult> CreatePt([FromBody] CreatePtAccountCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new
        {
            Message = "Tạo tài khoản PT thành công",
            AccountId = result,
            InputReceived = command
        });
    }

    // 4. Tạo tài khoản cho Staff
    [HttpPost("create-staff")]
    public async Task<IActionResult> CreateStaff([FromBody] CreateStaffAccountCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new
        {
            Message = "Tạo tài khoản Staff thành công",
            AccountId = result,
            InputReceived = command
        });
    }

    // 5. Lấy danh sách tất cả Staff và PT
    [HttpGet("staff-pts")]
    public async Task<IActionResult> GetAllStaffAndPts()
    {
        var result = await _mediator.Send(new GetStaffAndPtListQuery());

        if (result == null || !result.Any())
        {
            return Ok(new { Message = "Không có nhân sự nào trong danh sách", Data = new List<StaffAndPtDto>() });
        }
        return Ok(result);
    }

    // 6. Xóa nhân sự (Staff hoặc PT) theo danh sách ID
    [HttpDelete("delete-staff-pts")]
    public async Task<IActionResult> DeleteStaffOrPts([FromBody] List<Guid> ids)
    {
        var result = await _mediator.Send(new DeleteStaffOrPtCommand(ids));

        if (!result) return NotFound("Không tìm thấy nhân sự để xóa.");
        return Ok(new { Message = "Đã xóa thành công nhân sự đã chọn." });
    }
}
