using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongGym.Application.Common.Interfaces; // Cần dòng này
using QuanLyPhongGym.Application.Features.CheckIn.Commands.ScanQr;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace QuanLyPhongGym.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckInController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _context; // Đã thêm

        public CheckInController(IMediator mediator, IApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context; // Đã thêm
        }

        [Authorize]
        [HttpGet("my-qr-code")]
        public async Task<IActionResult> GetMyQrCode()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();

            // Bây giờ h.AccountId đã tồn tại sau khi bạn thêm vào Entity
            var hoiVien = await _context.HoiViens
                .FirstOrDefaultAsync(h => h.Id.ToString() == userIdClaim);

            if (hoiVien == null) return NotFound("Không tìm thấy hội viên khớp với tài khoản này.");

            return Ok(new { memberCode = hoiVien.MemberCode });
        }

        [Authorize(Roles = "Staff")]
        [HttpPost("scan-qr")]
        public async Task<IActionResult> ScanQr([FromBody] ScanQrCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        [Authorize(Roles = "Staff")]
        [HttpGet("dashboard-stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var today = DateTime.UtcNow.Date;

            // Đếm số lượng từ bảng CheckIns
            var totalCheckIn = await _context.CheckIns // Đổi từ CheckInLogs thành CheckIns (hoặc tên DbSet bạn đặt)
                .CountAsync(c => c.CheckInTime.Date == today);

            // Lấy danh sách 10 người check-in gần nhất
            var recentMembers = await _context.CheckIns
                .OrderByDescending(c => c.CheckInTime)
                .Take(10)
                .Select(c => new
                {
                    c.HoiVien.FirstName,
                    c.HoiVien.LastName,
                    c.CheckInTime
                })
                .ToListAsync();

            return Ok(new { totalCheckIn, recentMembers });
        }
    }
}