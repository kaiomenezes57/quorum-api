using MediatR;
using Quorum.Application.Interfaces;
using Quorum.Domain.Entities;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler(
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Guid>
    {
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                return default;

            var user = new User(
                request.Username, 
                request.Email);

            await userRepository.AddAsync(user);

            await unitOfWork.CommitAsync();

            return user.Id;
        }
    }
}
