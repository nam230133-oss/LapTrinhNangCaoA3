using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces; // BẮT BUỘC để dùng IApplicationDbContext
using QuanLyPhongGym.Application.Features.Admin.Commands.CreatePtAccount;
using QuanLyPhongGym.Application.Features.Admin.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.Admin.Commands.CreateAccount
{
    public class CreatePtAccountCommandHandler : IRequestHandler<CreatePtAccountCommand, Guid>
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _context; // 1. Khai báo biến _context

        // 2. Inject IApplicationDbContext vào constructor
        public CreatePtAccountCommandHandler(IMediator mediator, IApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<Guid> Handle(CreatePtAccountCommand request, CancellationToken cancellationToken) // 3. Đổi ct thành cancellationToken
        {
            // Kiểm tra trùng lặp
            // 4. Lúc này _context và cancellationToken đã được nhận diện
            var isExists = await _context.Accounts.AnyAsync(a => a.Username == request.Username, cancellationToken);

            if (isExists)
                throw new InvalidOperationException("Tên đăng nhập này đã tồn tại!");

            // Gọi Command chung (Common)
            var command = new CreateAccountCommand(
                request.Username, request.Password, request.FirstName, request.LastName,
                request.Email, request.PhoneNumber, "PT", request.Specialization
            );

            return await _mediator.Send(command, cancellationToken); // 5. Truyền đúng tham số
        }
    }
}