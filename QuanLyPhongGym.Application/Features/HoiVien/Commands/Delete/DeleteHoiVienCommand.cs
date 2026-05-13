using MediatR;

namespace QuanLyPhongGym.Application.Features.HoiVien.Commands.Delete;

// Command yêu cầu ID của hội viên cần xóa
public record DeleteHoiVienCommand(Guid Id) : IRequest<bool>;