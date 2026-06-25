using MediatR;
using Quorum.Application.Interfaces;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Options.Commands.CreateOption
{
    public sealed class CreateOptionCommandHandler(
        IPollRepository pollRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateOptionCommand, Guid>
    {
        public async Task<Guid> Handle(
            CreateOptionCommand request, 
            CancellationToken cancellationToken)
        {
            var poll = await pollRepository.GetByIdAsync(request.PollId);
            if (poll == null)
                return default;

            var option = poll.AddOption(request.Name);

            await unitOfWork.CommitAsync();

            return option.Id;
        }
    }
}
