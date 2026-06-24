using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongGym.Application.Features.Staff.Commands.AssignPackage; // Đảm bảo đường dẫn này khớp với project của bạn

namespace QuanLyPhongGym.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "NhanVien")] // Chỉ nhân viên mới có quyền truy cập
    public class StaffController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StaffController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AssignPackage")]
        public async Task<IActionResult> AssignPackage([FromBody] AssignPackageCommand command)
        {
            // Gửi command tới Handler để thực thi logic gán gói
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}