using MediatR;
using Quorum.Application.Shared.Responses;
using Quorum.Domain.Entities;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Auth.Register;

public class RegisterCommandHandler(IUserRepository repository) : 
    IRequestHandler<RegisterCommand, WebResponse<Guid>>
{
    public async Task<WebResponse<Guid>> Handle(
        RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await repository.GetByEmailAsync(request.Email);
        if (existingUser is not null)
            return WebResponse<Guid>
                .Failure("Given email has already been registered.");
        
        existingUser = await repository.GetByUsernameAsync(request.Name);
        if (existingUser is not null)
            return WebResponse<Guid>
                .Failure("Given username has already been registered.");

        var createdUser = new User(
            request.Name,
            request.Email,
            request.Password);
        
        await repository.CreateAsync(createdUser);

        return WebResponse<Guid>
            .Success(createdUser.Id);
    }
}