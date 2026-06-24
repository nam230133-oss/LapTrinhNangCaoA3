namespace QuanLyPhongGym.Application.Features.CheckIn.Commands.ScanQr
{
    public class ScanQrResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? MemberName { get; set; }
    }
}