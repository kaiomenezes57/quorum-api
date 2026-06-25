using MediatR;
using Quorum.Application.Interfaces;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Votes.Commands.CreateVote
{
    public sealed class CreateVoteCommandHandler(
        IUserRepository userRepository,
        IPollRepository pollRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateVoteCommand, Guid>
    {
        public async Task<Guid> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.VoterId);
            var poll = await pollRepository.GetByIdAsync(request.PollId);

            if (user is null || poll is null)
                return default;

            var vote = poll.AddVote(request.VoterId, request.OptionId);

            await unitOfWork.CommitAsync();

            return vote.Id;
        }
    }
}
