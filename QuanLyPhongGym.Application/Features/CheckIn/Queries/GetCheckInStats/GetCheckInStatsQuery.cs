using MediatR;

namespace QuanLyPhongGym.Application.Features.CheckIn.Queries.GetCheckInStats
{
    public record GetCheckInStatsQuery() : IRequest<int>;
}