// File: Features/HoiVien/Commands/Create/CreateHoiVienCommandHandler.cs
using MediatR;
using QuanLyPhongGym.Application.Common.Interfaces;

namespace QuanLyPhongGym.Application.Features.HoiVien.Commands.Create
{
    public class CreateHoiVienCommandHandler : IRequestHandler<CreateHoiVienCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateHoiVienCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateHoiVienCommand request, CancellationToken cancellationToken)
        {
            // 1. Chuyển đổi dữ liệu Command thành Entity của tầng Domain
            var entity = new Domain.Entities.HoiVien
            {
                Id = Guid.NewGuid(),
                Ten = request.Ten,
                SoDienThoai = request.SoDienThoai,
                NgayThamGia = DateTime.UtcNow,
                TrangThaiHoatDong = true
            };

            // 2. Thêm vào DbContext và Lưu
            _context.HoiViens.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            // 3. Trả về Id
            return entity.Id;
        }
    }
}