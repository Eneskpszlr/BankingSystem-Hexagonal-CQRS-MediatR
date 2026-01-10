using BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches;
using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Domain.ValueObjects;

namespace BankingHexagonal.Application.UseCases.Branches
{
    public class UpdateBranchUseCase : IUpdateBranchUseCase
    {
        private readonly IBranchRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBranchUseCase(IBranchRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(UpdateBranchCommand command)
        {
            var branch = await _repository.GetByIdAsync(command.Id);
            if (branch == null) throw new Exception("Şube bulunamadı");

            // 1. Yeni Adres Nesnesi
            var newAddress = new Address(
                command.Street,
                command.City,
                command.Country,
                command.ZipCode
            );

            // 2. Domain Metodu ile Güncelleme
            branch.UpdateDetails(command.BranchName, newAddress);

            // 3. Kaydet
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
