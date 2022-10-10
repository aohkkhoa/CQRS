using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Wrapper;

namespace Application.Features.AuthFeatures.Commands.Update
{
    public class ForgotPasswordCommand : IRequest<Result<string>>
    {
        public string email { get; set; }
    }
    public class ForgotPasswordCommandHandle : IRequestHandler<ForgotPasswordCommand, Result<string>>
    {
        private readonly IAuthRepository _authRepository;
        public ForgotPasswordCommandHandle(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<Result<string>> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
        {
            _authRepository.ForgotPassWord("ndk1031999@gmail.com");
            return await Result<string>.SuccessAsync("sendmail Sucess");
        }
    }
}

