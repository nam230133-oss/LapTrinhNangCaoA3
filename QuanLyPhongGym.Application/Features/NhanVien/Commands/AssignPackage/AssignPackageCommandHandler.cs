using MediatR;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.Staff.Commands.AssignPackage
{
    public class AssignPackageCommandHandler : IRequestHandler<AssignPackageCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public AssignPackageCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(AssignPackageCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra staffId null để tránh lỗi Parse
            if (!Guid.TryParse(_currentUserService.UserId, out var staffId))
            {
                throw new UnauthorizedAccessException("Không tìm thấy thông tin nhân viên đăng nhập.");
            }

            var contract = new Contract
            {
                Id = Guid.NewGuid(),
                HoiVienId = request.HoiVienId,
                GoiTapId = request.GoiTapId,
                NhanVienId = staffId,
                NgayTao = DateTime.UtcNow
            };

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync(cancellationToken);

            return contract.Id;
        }
    }
}