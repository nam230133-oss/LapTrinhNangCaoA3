using MediatR;
using System;

namespace QuanLyPhongGym.Application.Features.TheHoiVien.Commands.CreateTheHoiVien
{
    // Lệnh yêu cầu truyền vào ID hội viên và ID gói tập, trả về ID của Thẻ sau khi tạo thành công
    public record CreateTheHoiVienCommand(Guid HoiVienId, Guid GoiTapId) : IRequest<Guid>;
}