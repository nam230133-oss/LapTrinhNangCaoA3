using MediatR;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.HoiVien.Commands.Create
{

    public class CreateHoiVienCommandHandler : IRequestHandler<CreateHoiVienCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        // Sử dụng Block Constructor chuẩn để tránh lỗi nhận diện của compiler
        public CreateHoiVienCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateHoiVienCommand request, CancellationToken cancellationToken)
        {
            var hoiVien = new Domain.Entities.HoiVien
            {
                Id = Guid.NewGuid(),
                MemberCode = request.MemberCode,
                LastName = request.LastName,
                FirstName = request.FirstName,
                Phone = request.Phone,
                Email = request.Email,
                CreatedDate = DateTime.Now,
                Status = true
            };

            _context.HoiViens.Add(hoiVien);
            await _context.SaveChangesAsync(cancellationToken);

            return hoiVien.Id;
        }
    }
}