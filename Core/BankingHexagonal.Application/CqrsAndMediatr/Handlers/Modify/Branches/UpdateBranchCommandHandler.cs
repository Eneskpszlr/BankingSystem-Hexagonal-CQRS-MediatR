using BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches;
using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Branches;
using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Modify.Branches
{
    public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, UpdateBranchCommandResult>
    {
        private readonly IUpdateBranchUseCase _useCase;

        public UpdateBranchCommandHandler(IUpdateBranchUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<UpdateBranchCommandResult> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            await _useCase.ExecuteAsync(request);
            return new UpdateBranchCommandResult
            {
                Message = "Şube başarıyla güncellendi."
            };
        }
    }
}
