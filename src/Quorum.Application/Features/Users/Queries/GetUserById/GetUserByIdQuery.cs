using MediatR;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<UserResponseDto?>;

    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponseDto?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseDto?> Handle(
            GetUserByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
                return null;

            return new UserResponseDto(
                user.Username, 
                user.Email, 
                user.Polls.Select(p => p.Id).ToList().AsReadOnly());
        }
    }
}
