using BankingHexagonal.Application.CqrsAndMediatr.Queries.Branches;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Branches;
using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Read.Branches
{
    public class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, GetBranchByIdQueryResult>
    {
        private readonly IGetBranchByIdUseCase _useCase;
        public GetBranchByIdQueryHandler(IGetBranchByIdUseCase useCase)
        {
            _useCase = useCase;
        }
        public async Task<GetBranchByIdQueryResult> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            var branch = await _useCase.ExecuteAsync(request.Id);

            return new GetBranchByIdQueryResult
            {
                Id = branch.Id,
                BranchName = branch.BranchName,

                // --- VALUE OBJECT MAPPING ---
                Street = branch.Address.Street,
                City = branch.Address.City,
                Country = branch.Address.Country,
                ZipCode = branch.Address.ZipCode
            };
        }
    }
}
