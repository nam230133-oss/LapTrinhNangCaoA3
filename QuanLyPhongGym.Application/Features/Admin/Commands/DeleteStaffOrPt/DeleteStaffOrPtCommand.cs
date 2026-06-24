using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyPhongGym.Application.Features.Admin.Commands.DeleteStaffOrPt
{
    public record DeleteStaffOrPtCommand(List<Guid> Ids) : IRequest<bool>;
}
