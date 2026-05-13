using MediatR;
// Sử dụng alias (biệt danh) để tránh nhầm lẫn
using HoiVienEntity = QuanLyPhongGym.Domain.Entities.HoiVien;

namespace QuanLyPhongGym.Application.Features.HoiVien.Queries.GetList;

public record GetHoiVienListQuery() : IRequest<List<HoiVienEntity>>;