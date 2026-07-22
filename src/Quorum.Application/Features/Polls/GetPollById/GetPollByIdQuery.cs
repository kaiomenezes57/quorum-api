using MediatR;
using Quorum.Application.Shared.Polls;
using Quorum.Application.Shared.Responses;

namespace Quorum.Application.Features.Polls.GetPollById;

public record GetPollByIdQuery(Guid PollId) : 
    IRequest<WebResponse<PollResponseDto?>>;