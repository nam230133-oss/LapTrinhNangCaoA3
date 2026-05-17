using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using QuanLyPhongGym.Application.Features.HoiVien.Commands.Create;
using QuanLyPhongGym.Application.Features.HoiVien.Commands.Delete;
using QuanLyPhongGym.Application.Features.TheHoiVien.Commands.CreateTheHoiVien;
using QuanLyPhongGym.Application.Features.HoiVienManagement.Queries.GetList;
using QuanLyPhongGym.Application.Features.HoiVienManagement.Queries.GetById;
using QuanLyPhongGym.Application.Features.HoiVienManagement.Queries.GetWithPaginatedList;
using QuanLyPhongGym.Application.Features.HoiVienManagement.Queries.GetMemberSubscriptions;
using QuanLyPhongGym.Application.Features.HoiVien.Commands.Update;

namespace QuanLyPhongGym.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HoiVienController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HoiVienController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // 1. Thêm mới hội viên
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHoiVienCommand command)
        {
            var newId = await _mediator.Send(command);
            return Ok(new
            {
                message = "Thêm hội viên thành công",
                id = newId
            });
        }

        // 2. Lấy danh sách toàn bộ hội viên (Không phân trang)
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetHoiVienListQuery());
            return Ok(result);
        }

        // 3. Xem chi tiết 1 hội viên theo ID (Mới thêm)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetHoiVienByIdQuery(id));
            if (result == null)
            {
                return NotFound(new { message = "Không tìm thấy hội viên này trong hệ thống!" });
            }
            return Ok(result);
        }

        // 4. Lấy danh sách hội viên phân trang & tìm kiếm (Mới thêm)
        [HttpGet("Paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            var result = await _mediator.Send(new GetHoiVienPaginatedQuery(pageNumber, pageSize, searchTerm));
            return Ok(result);
        }

        // 5. Xóa hội viên
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteHoiVienCommand(id));

            if (!result)
            {
                return NotFound(new { message = "Không tìm thấy hội viên để xóa" });
            }

            return Ok(new { message = "Xóa hội viên thành công" });
        }

        // 6. Đăng ký gói tập cho hội viên (Đã thông suốt với folder TheHoiVien)
        [HttpPost("DangKyGoi")]
        public async Task<IActionResult> DangKyGoi([FromBody] CreateTheHoiVienCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(new
            {
                message = "Đăng ký thẻ hội viên thành công",
                subscriptionId = result
            });
        }

        // 7. Lấy danh sách gói tập đã đăng ký của hội viên (Có hiển thị tên riêng, gói riêng)
        [HttpGet("{id}/subscriptions")]
        public async Task<IActionResult> GetMemberSubscriptions(Guid id)
        {
            return Ok(await _mediator.Send(new GetMemberSubscriptionsQuery(id)));
        }
        // Chức năng: Cập nhật thông tin hội viên
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateHoiVienCommand command)
        {
            // Bảo mật: Kiểm tra xem Id trên link URL và Id trong cục JSON gửi lên có khớp nhau không
            if (id != command.Id)
            {
                return BadRequest(new { message = "Mã ID trên URL và trong dữ liệu không trùng khớp!" });
            }

            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound(new { message = "Không tìm thấy hội viên này để cập nhật!" });
            }

            return Ok(new { message = "Cập nhật thông tin hội viên thành công!" });
        }
    }
}