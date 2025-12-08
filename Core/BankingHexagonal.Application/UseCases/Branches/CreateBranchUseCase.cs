using BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches;
using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.UseCases.Branches
{
    public class CreateBranchUseCase : ICreateBranchUseCase
    {
        private readonly IBranchRepository _repository;

        public CreateBranchUseCase(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(CreateBranchCommand command)
        {
            Branch branch = new Branch
            {
                BranchName = command.BranchName,
                Address = command.Address,
                CreatedDate = DateTime.Now,
                Status = Domain.Enums.DataStatus.Inserted
            };
            await _repository.CreateAsync(branch);
        }
    }
}
