using Quorum.Application.Features.Polls.GetAllPolls;
using Quorum.Application.Shared.Options;
using Quorum.Domain.Entities;

namespace Quorum.Application.Shared.Polls;

public static class PollExtensions
{
    public static PollResponseDto ToDto(this Poll source)
    {
        var pollResponse = new PollResponseDto(
            source.Id,
            source.UserId,
            source.Name,
            source.Description,
            source.CreatedAt,
            source.LastUpdatedAt,
            source.Options 
                .Select(o => new OptionResponseDto(o.Id, o.Name)) 
                .ToList()
                .AsReadOnly()
        );

        return pollResponse;
    }
}