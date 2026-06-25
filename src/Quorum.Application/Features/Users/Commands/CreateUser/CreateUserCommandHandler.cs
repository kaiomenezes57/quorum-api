using MediatR;
using Quorum.Application.Interfaces;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                return default;

            var user = new Domain.Entities.User(
                request.Username, 
                request.Email);

            await _userRepository.AddAsync(user);

            await _unitOfWork.CommitAsync();

            return user.Id;
        }
    }
}
