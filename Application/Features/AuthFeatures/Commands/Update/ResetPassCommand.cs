using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Wrapper;

namespace Application.Features.AuthFeatures.Commands.Update
{
    public class ResetPassCommand : IRequest<Result<string>>
    {
        public string token { get; set; }
        public string newPassword { get; set; }
    }
    public class ResetPassCommandHandle : IRequestHandler<ResetPassCommand, Result<string>>
    {

        private readonly IAuthRepository _authRepository;

        public ResetPassCommandHandle(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<Result<string>> Handle(ResetPassCommand command, CancellationToken cancellationToken)
        {
            await _authRepository.ResetPassWord(command.token, command.newPassword);
            return await Result<string>.SuccessAsync("compelete");
        }

    }
}
