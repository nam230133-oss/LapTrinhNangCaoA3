using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using QuanLyPhongGym.Application.Common.Interfaces;

namespace QuanLyPhongGym.Infrastructure.Services // <--- Namespace phải khớp chính xác
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                // Ghi log hoặc debug tại đây để xem các claims đang có
                var claims = user?.Claims.ToList();

                // Thử tìm theo nhiều kiểu nếu NameIdentifier không thấy
                return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? user?.FindFirst("uid")?.Value
                    ?? user?.FindFirst("id")?.Value;
            }
        }
    }
}