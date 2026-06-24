using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyPhongGym.Application.Features.Auth.Commands.ActivateAccount;

public record ActivateAccountCommand(string Phone, string Otp, string Password) : IRequest<bool>;