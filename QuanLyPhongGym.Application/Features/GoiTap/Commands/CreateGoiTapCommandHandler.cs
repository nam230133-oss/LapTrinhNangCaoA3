using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Domain.Entities;

namespace QuanLyPhongGym.Application.Features.GoiTaps.Commands
{
    public class CreateGoiTapCommandHandler : IRequestHandler<CreateGoiTapCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateGoiTapCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<Guid> Handle(CreateGoiTapCommand request, CancellationToken cancellationToken)
        {
            // 1. Kiểm tra xem tên gói đã tồn tại chưa
            var isExists = await _context.GoiTaps
                .AnyAsync<QuanLyPhongGym.Domain.Entities.GoiTap>(g => g.TenGoi == request.TenGoi, cancellationToken);

            if (isExists)
            {
                // Bạn có thể ném ra một Exception tùy chỉnh hoặc trả về lỗi
                throw new InvalidOperationException("Gói này đã có trong dữ liệu! Hãy điền lại thông tin mới hơn.");
            }

            // 2. Nếu không trùng thì mới tạo mới
            var goiTap = new QuanLyPhongGym.Domain.Entities.GoiTap
            {
                Id = Guid.NewGuid(),
                TenGoi = request.TenGoi,
                ThoiHanNgay = request.ThoiHanNgay,
                Gia = request.Gia,
                MoTa = request.MoTa,
                SoBuoi = request.SoBuoi
            };

            _context.GoiTaps.Add(goiTap);
            await _context.SaveChangesAsync(cancellationToken);

            return goiTap.Id;
        }
    }
}