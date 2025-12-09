using BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches;
using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Branches;
using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Modify.Branches
{
    public class RemoveBranchCommandHandler : IRequestHandler<RemoveBranchCommand, RemoveBranchCommandResult>
    {
        private readonly IRemoveBranchUseCase _useCase;
        public RemoveBranchCommandHandler(IRemoveBranchUseCase useCase)
        {
            _useCase = useCase;
        }
        public async Task<RemoveBranchCommandResult> Handle(RemoveBranchCommand request, CancellationToken cancellationToken)
        {
            await _useCase.ExecuteAsync(request.Id);
            return new RemoveBranchCommandResult
            {
                Message = "Şube başarıyla silindi."
            };
        }
    }
}
