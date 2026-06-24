using FluentValidation;

namespace QuanLyPhongGym.Application.Features.HoiVien.Commands.Create
{
    public class CreateHoiVienCommandValidator : AbstractValidator<CreateHoiVienCommand>
    {
        public CreateHoiVienCommandValidator()
        {
            // Các trường bắt buộc phải có giá trị
            RuleFor(v => v.FirstName)
                .NotEmpty().WithMessage("Tên không được để trống.");

            RuleFor(v => v.LastName)
                .NotEmpty().WithMessage("Họ không được để trống.");

            RuleFor(v => v.Phone)
                .NotEmpty().WithMessage("Số điện thoại không được để trống.")
                .Matches(@"^0\d{9}$").WithMessage("Số điện thoại không hợp lệ (phải bắt đầu bằng 0 và có 10 chữ số).");

            // MemberCode là tùy chọn (vì hệ thống tự sinh) nên không cần RuleFor(v => v.MemberCode).NotEmpty();
        }
    }
}