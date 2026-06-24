using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyPhongGym.Application.Features.Admin.Commands.CreatePtAccount
{
    public class CreatePtAccountCommandValidator : AbstractValidator<CreatePtAccountCommand>
    {
        public CreatePtAccountCommandValidator()
        {
            RuleFor(v => v.Username).NotEmpty().MaximumLength(50);
            RuleFor(v => v.Specialization).NotEmpty().WithMessage("PT bắt buộc phải có chuyên môn");
        }
    }
}
