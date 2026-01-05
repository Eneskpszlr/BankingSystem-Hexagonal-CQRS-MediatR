using BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches;
using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Branches;
using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Modify.Branches
{
    public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, CreateBranchCommandResult>
    {
        private readonly ICreateBranchUseCase _useCase;

        public CreateBranchCommandHandler(ICreateBranchUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<CreateBranchCommandResult> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            int createdBranchId = await _useCase.ExecuteAsync(request);

            return new CreateBranchCommandResult
            {
                Success = true,
                Message = "Şube başarıyla oluşturuldu.",
                EntityId = createdBranchId
            };
        }
    }
}
