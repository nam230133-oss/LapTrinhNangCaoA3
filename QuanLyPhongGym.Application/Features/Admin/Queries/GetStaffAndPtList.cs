using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyPhongGym.Application.Features.Admin.Queries
{
    public record GetStaffAndPtListQuery : IRequest<List<StaffAndPtDto>>;
}
