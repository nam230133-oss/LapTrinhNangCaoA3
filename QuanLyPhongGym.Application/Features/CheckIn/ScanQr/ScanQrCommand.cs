using MediatR;

namespace QuanLyPhongGym.Application.Features.CheckIn.Commands.ScanQr
{
    public record ScanQrCommand(string MemberCode) : IRequest<ScanQrResponse>;
}