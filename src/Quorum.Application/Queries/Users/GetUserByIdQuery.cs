using MediatR;
using Quorum.Application.DTOs.Users;
using Quorum.Application.Interfaces;

namespace Quorum.Application.Queries.Users
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
