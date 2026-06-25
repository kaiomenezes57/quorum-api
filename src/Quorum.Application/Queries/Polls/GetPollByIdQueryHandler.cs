using MediatR;
using Quorum.Application.DTOs.Options;
using Quorum.Application.DTOs.Polls;
using Quorum.Application.DTOs.Votes;
using Quorum.Application.Interfaces;

namespace Quorum.Application.Queries.Polls
{
    public sealed class GetPollByIdQueryHandler : IRequestHandler<GetPollByIdQuery, PollResponseDto?>
    {
        private readonly IPollRepository _pollRepository;

        public GetPollByIdQueryHandler(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public async Task<PollResponseDto?> Handle(GetPollByIdQuery request, CancellationToken cancellationToken)
        {
            var poll = await _pollRepository.GetByIdAsync(request.Id);
            if (poll == null)
                return null;

            return new PollResponseDto(
                poll.Name,
                poll.Description,
                poll.OwnerId,
                (int)poll.Status,
                poll.VotesTarget,
                poll.CreateDate,
                poll.LastModifierDate,
                poll.EndDate,
                poll.Options.Select(x => new OptionResponseDto(x.Name)).ToList().AsReadOnly(),
                poll.Votes.Select(x => new VoteResponseDto(x.VoterId, x.PollId, x.OptionId, x.VoteDate)).ToList().AsReadOnly());
        }
    }
}
