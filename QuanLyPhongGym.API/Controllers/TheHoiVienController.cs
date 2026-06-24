using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongGym.Application.Features.TheHoiVien.Commands.CreateTheHoiVien;
using QuanLyPhongGym.Application.Features.TheHoiVien.Queries.GetList; // Đảm bảo bạn đã tạo Query này
using System;
using System.Threading.Tasks;

namespace QuanLyPhongGym.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TheHoiVienController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TheHoiVienController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/TheHoiVien/GetList
        // Nhân viên/Admin xem danh sách các thẻ đang có
        [Authorize(Roles = "Admin, Staff")]
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetTheHoiVienListQuery());
            return Ok(result);
        }

        // POST: api/TheHoiVien/Create
        // Nhân viên/Admin gán gói tập cho hội viên
        [Authorize(Roles = "Admin, Staff")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateTheHoiVienCommand command)
        {
            try
            {
                var newId = await _mediator.Send(command);
                return Ok(new
                {
                    message = "Gán gói tập cho hội viên thành công!",
                    theHoiVienId = newId
                });
            }
            catch (InvalidOperationException ex)
            {
                // Bắt các lỗi logic (ví dụ: đã có thẻ còn hạn)
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Bắt các lỗi không mong muốn
                return StatusCode(500, new { message = "Đã xảy ra lỗi hệ thống: " + ex.Message });
            }
        }


        // 2 Gán gói tập cho hội viên (Chỉ Admin và Staff mới được phép gán)
        [Authorize(Roles = "Admin, Staff")]
        [HttpPost("GanGoiTap")]
        public async Task<IActionResult> GanGoiTap([FromBody] CreateTheHoiVienCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { message = "Đã gán gói tập thành công!", theHoiVienId = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
