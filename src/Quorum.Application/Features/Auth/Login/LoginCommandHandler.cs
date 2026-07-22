using MediatR;
using Quorum.Application.Interfaces;
using Quorum.Application.Shared.Responses;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Auth.Login;

public class LoginCommandHandler(IUserRepository repository, ITokenService tokenService) : 
    IRequestHandler<LoginCommand, WebResponse<LoginResponseDto>>
{
    public async Task<WebResponse<LoginResponseDto>> Handle(
        LoginCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await repository.GetByEmailAsync(request.Email);
        if (user is null)
            return WebResponse<LoginResponseDto>
                .Failure("Given email is not registered.")!;

        if (!user.VerifyPassword(request.Password))
            return WebResponse<LoginResponseDto>
                .Failure("Given password is not correct.")!;
        
        var token = tokenService.Generate(user.Id);
        
        return WebResponse<LoginResponseDto>
            .Success(new LoginResponseDto(user.Id, token))!;
    }
}