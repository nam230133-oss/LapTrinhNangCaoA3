using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Domain.Entities;

namespace QuanLyPhongGym.Application.Features.LichTaps.Commands.CreateLichTap;

public class CreateLichTapCommandHandler : IRequestHandler<CreateLichTapCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateLichTapCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateLichTapCommand request, CancellationToken cancellationToken)
    {
        // Logic kiểm tra trùng lịch
        var isOverlap = await _context.LichTaps.AnyAsync(l =>
            l.ThoiGianBatDau < request.ThoiGianKetThuc &&
            l.ThoiGianKetThuc > request.ThoiGianBatDau,
            cancellationToken);

        if (isOverlap)
            throw new Exception("Huấn luyện viên đã bận vào khung giờ này!");

        var entity = new LichTap
        {
            Id = Guid.NewGuid(),
            HoiVienId = request.HoiVienId,
            ThoiGianBatDau = request.ThoiGianBatDau,
            ThoiGianKetThuc = request.ThoiGianKetThuc
        };

        _context.LichTaps.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}