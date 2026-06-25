using MediatR;
using Quorum.Application.Interfaces;
using Quorum.Domain.Entities;

namespace Quorum.Application.Commands.Options
{
    public sealed class CreateOptionCommandHandler : IRequestHandler<CreateOptionCommand, Guid>
    {
        private readonly IOptionRepository _optionRepository;
        private readonly IPollRepository _pollRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOptionCommandHandler(
            IOptionRepository optionRepository, 
            IPollRepository pollRepository, 
            IUnitOfWork unitOfWork)
        {
            _optionRepository = optionRepository;
            _pollRepository = pollRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateOptionCommand request, CancellationToken cancellationToken)
        {
            var poll = await _pollRepository.GetByIdAsync(request.PollId);
            if (poll == null)
                return default;

            var option = new Option(
                request.Name,
                poll);

            await _optionRepository.AddAsync(option);

            await _unitOfWork.CommitAsync();

            return option.Id;
        }
    }
}
