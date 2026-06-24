using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Application.Features.Auth.Commands.ActivateAccount;
using QuanLyPhongGym.Application.Features.Auth.Commands.Login;
using QuanLyPhongGym.Application.Features.Auth.Commands.Register;
using System;
using System.Threading.Tasks;

namespace QuanLyPhongGym.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _context;

        // Constructor: Inject cả Mediator và DbContext
        public AuthController(IMediator mediator, IApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            // Đảm bảo LoginResponse ở đây được hiểu là cái nằm trong namespace trên
            LoginResponse response = await _mediator.Send(command);

            if (!response.IsSuccess)
                return BadRequest(new { message = response.Message });

            return Ok(response);
        }
        [HttpPost("request-otp")]
        public async Task<ActionResult<OtpResponse>> RequestOtp([FromBody] OtpRequest request)
        => Ok(await _mediator.Send(request));

        // Bước 2: Trả về Token2
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpCommand command)
        {
            var token2 = await _mediator.Send(command);
            return Ok(new { token2 = token2 });
        }

        // Bước 3: Đăng ký, trả về Token3 hoặc Success
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response.IsSuccess) return BadRequest(new { message = response.Message });
            return Ok(response);
        }

        [HttpDelete("delete-user/{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            // Truy vấn user bằng _context đã được Inject
            var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return NotFound(new { message = "Không tìm thấy tài khoản!" });

            _context.Accounts.Remove(user);
            await _context.SaveChangesAsync(default); // Lưu thay đổi xuống Database

            return Ok(new { message = $"Đã xóa tài khoản {username} thành công!" });
        }
        [HttpPost("activate")]
        public async Task<IActionResult> Activate([FromBody] ActivateAccountCommand command)
        {
            // Gọi MediatR
            var result = await _mediator.Send(command);
            return Ok(new { message = "Kích hoạt tài khoản thành công!", success = result });
        }
    }
}