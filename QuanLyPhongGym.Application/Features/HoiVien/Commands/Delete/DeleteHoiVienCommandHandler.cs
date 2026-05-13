using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;

namespace QuanLyPhongGym.Application.Features.HoiVien.Commands.Delete;

public class DeleteHoiVienCommandHandler : IRequestHandler<DeleteHoiVienCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteHoiVienCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteHoiVienCommand request, CancellationToken cancellationToken)
    {
        // 1. Tìm hội viên trong Database
        var hoiVien = await _context.HoiViens
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (hoiVien == null) return false; // Không tìm thấy để xóa

        // 2. Thực hiện xóa
        _context.HoiViens.Remove(hoiVien);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}