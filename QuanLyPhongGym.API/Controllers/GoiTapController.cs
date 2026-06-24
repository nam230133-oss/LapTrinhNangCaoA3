using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongGym.Application.Features.GoiTap.Queries.GetList;
using QuanLyPhongGym.Application.Features.GoiTaps.Commands;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace QuanLyPhongGym.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoiTapController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GoiTapController(IMediator mediator) => _mediator = mediator;

        // Chỉ Admin mới được phép tạo gói tập mới
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateGoiTapCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new
                {
                    message = "Tạo gói tập thành công",
                    packageId = result
                });
            }
            catch (InvalidOperationException ex)
            {
                // Trả về lỗi 400 (Bad Request) thay vì để hệ thống tự văng lỗi 500
                return BadRequest(new { message = ex.Message });
            }
        }

        // Cả Admin và Staff đều có thể xem danh sách để gán cho hội viên
        [Authorize(Roles = "Admin, Staff")]
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetGoiTapListQuery());
            return Ok(result);
        }
    }
}