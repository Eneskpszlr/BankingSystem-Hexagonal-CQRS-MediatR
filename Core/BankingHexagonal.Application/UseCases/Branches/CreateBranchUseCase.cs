using BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches;
using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Domain.ValueObjects;
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
        private readonly IUnitOfWork _unitOfWork;

        public CreateBranchUseCase(IBranchRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> ExecuteAsync(CreateBranchCommand command)
        {
            // 1. Value Object Oluşturma
            var address = new Address(
                command.Street,
                command.City,
                command.Country,
                command.ZipCode
            );

            // 2. Entity Oluşturma (Constructor Kullanımı)
            var branch = new Branch(command.BranchName, address);

            // 3. Repo ve Kayıt
            await _repository.CreateAsync(branch);
            await _unitOfWork.SaveChangesAsync();

            return branch.Id;
        }
    }
}
