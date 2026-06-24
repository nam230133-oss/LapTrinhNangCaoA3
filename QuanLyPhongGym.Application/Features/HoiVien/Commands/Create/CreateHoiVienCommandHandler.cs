using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.HoiVien.Commands.Create
{
    public class CreateHoiVienCommandHandler : IRequestHandler<CreateHoiVienCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateHoiVienCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateHoiVienCommand request, CancellationToken cancellationToken)
        {
            // Thêm <Domain.Entities.HoiVien> vào sau AnyAsync
            var isPhoneExists = await _context.HoiViens
                .AnyAsync<Domain.Entities.HoiVien>(h => h.Phone == request.Phone, cancellationToken);

            if (isPhoneExists)
                throw new InvalidOperationException("Thông tin trên đã được đăng ký cho hội viên khác! Hãy điền thông tin mới.");
            if (!Guid.TryParse(_currentUserService.UserId, out var staffId))
            {
                throw new UnauthorizedAccessException("Bạn cần đăng nhập với quyền nhân viên.");
            }

            var hoiVien = new Domain.Entities.HoiVien
            {
                Id = Guid.NewGuid(),
                MemberCode = request.MemberCode,
                LastName = request.LastName,
                FirstName = request.FirstName,
                Phone = request.Phone,
                Email = request.Email,
                Gender = request.Gender,
                NhanVienTaoId = staffId,
                Status = "Active" // Set the default status to "Active"

            };

            _context.HoiViens.Add(hoiVien);
            await _context.SaveChangesAsync(cancellationToken);

            return hoiVien.Id;
        }
    }
}