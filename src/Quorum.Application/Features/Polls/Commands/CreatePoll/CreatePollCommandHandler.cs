using MediatR;
using Quorum.Application.Interfaces;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Polls.Commands.CreatePoll
{
    public sealed class CreatePollCommandHandler : IRequestHandler<CreatePollCommand, Guid>
    {
        private readonly IPollRepository _pollRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePollCommandHandler(
            IPollRepository pollRepository, 
            IUserRepository userRepository, 
            IUnitOfWork unitOfWork)
        {
            _pollRepository = pollRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreatePollCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.OwnerId);
            if (user == null)
                return default;

            var poll = new Domain.Entities.Poll(
                request.Name,
                request.Description,
                user,
                request.VotesTarget);

            await _pollRepository.AddAsync(poll);

            await _unitOfWork.CommitAsync();

            return poll.Id;
        }
    }
}
