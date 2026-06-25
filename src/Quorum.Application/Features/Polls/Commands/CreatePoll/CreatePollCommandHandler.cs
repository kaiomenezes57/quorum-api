using MediatR;
using Quorum.Application.Interfaces;
using Quorum.Domain.Entities;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Polls.Commands.CreatePoll
{
    public sealed class CreatePollCommandHandler(
        IPollRepository pollRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreatePollCommand, Guid>
    {
        public async Task<Guid> Handle(CreatePollCommand request, CancellationToken cancellationToken)
        {
            var poll = new Poll(
                request.Name,
                request.Description,
                request.OwnerId,
                request.VotesTarget);

            await pollRepository.AddAsync(poll);

            await unitOfWork.CommitAsync();

            return poll.Id;
        }
    }
}
