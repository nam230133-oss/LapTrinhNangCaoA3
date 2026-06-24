using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuanLyPhongGym.Application.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuanLyPhongGym.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IApplicationDbContext _context;

        public LoginCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // 1. Truy vấn tài khoản
            var account = await _context.Accounts
    .FirstOrDefaultAsync(a => a.Username.Trim() == request.Username.Trim(), cancellationToken);

            if (account == null)
            {
                return new LoginResponse { IsSuccess = false, Message = "Tên đăng nhập không tồn tại!" };
            }

            // 2. Kiểm tra mật khẩu bằng BCrypt
            bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, account.PasswordHash);

            if (!isValid)
            {
                return new LoginResponse { IsSuccess = false, Message = "Tài khoản hoặc mật khẩu không chính xác!" };
            }

            // 3. Logic sinh JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            // Lưu ý: Key phải có độ dài ít nhất 16-32 ký tự để đảm bảo bảo mật (HmacSha256)
            var key = Encoding.ASCII.GetBytes("Sieu_Mat_Khau_Bao_Mat_Phong_Gym_Tich_Hop_AI_2026");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, account.Role ?? "Member") // Xử lý null an toàn
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // 4. Trả về kết quả
            return new LoginResponse
            {
                IsSuccess = true,
                Message = "Đăng nhập thành công!",
                Token = tokenString,
                Username = account.Username,
                Role = account.Role
            };
        }
    }
}