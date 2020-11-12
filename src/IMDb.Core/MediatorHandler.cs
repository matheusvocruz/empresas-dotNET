using FluentValidation.Results;
using IMDb.Core.Interfaces;
using MediatR;
using System.Threading.Tasks;

namespace IMDb.Core
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}
