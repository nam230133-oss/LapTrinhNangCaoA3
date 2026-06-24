using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyPhongGym.Application.Features.Staff.Commands.AssignPackage
{
    public record AssignPackageCommand(Guid HoiVienId, Guid GoiTapId) : IRequest<Guid>;
}
