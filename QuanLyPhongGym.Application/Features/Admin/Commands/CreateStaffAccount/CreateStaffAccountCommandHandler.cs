using MediatR;
using QuanLyPhongGym.Application.Features.Admin.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.Admin.Commands.CreateStaffAccount
{
    public class CreateStaffAccountCommandHandler : IRequestHandler<CreateStaffAccountCommand, Guid>
    {
        private readonly IMediator _mediator;

        public CreateStaffAccountCommandHandler(IMediator mediator) => _mediator = mediator;

        public async Task<Guid> Handle(CreateStaffAccountCommand request, CancellationToken ct)
        {
            // 1. Tạo Command chung
            var command = new CreateAccountCommand(
                request.Username,
                request.Password,
                request.FirstName,
                request.LastName,
                request.Email,
                request.PhoneNumber,
                "Staff", // Role cứng cho Staff
                null     // Staff không cần Specialization
            );

            // 2. Gọi Mediator để xử lý (nó sẽ gọi tới CreateAccountCommandHandler gốc)
            // Mediator.Send trả về Guid của Account vừa tạo
            Guid accountId = await _mediator.Send(command, ct);

            // 3. Return Id này về cho Controller
            return accountId;
        }
    }
}