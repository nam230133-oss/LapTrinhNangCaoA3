using MediatR;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Domain.Entities;

namespace QuanLyPhongGym.Application.Features.GoiTaps.Commands
{
    public record CreateGoiTapCommand(
        string TenGoi,
        int ThoiHanNgay,
        decimal Gia,
        string? MoTa,
        int? SoBuoi) : IRequest<Guid>;

    public class CreateGoiTapCommandHandler : IRequestHandler<CreateGoiTapCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateGoiTapCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateGoiTapCommand request, CancellationToken cancellationToken)
        {
            var entity = new GoiTap
            {
                Id = Guid.NewGuid(),
                TenGoi = request.TenGoi,
                ThoiHanNgay = request.ThoiHanNgay,
                Gia = request.Gia,
                MoTa = request.MoTa,
                SoBuoi = request.SoBuoi,
                TrangThai = true
            };

            _context.GoiTaps.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}