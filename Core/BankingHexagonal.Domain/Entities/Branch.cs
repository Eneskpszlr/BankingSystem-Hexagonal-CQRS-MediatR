using BankingHexagonal.Domain.Entities.Base;
using BankingHexagonal.Domain.Enums;
using BankingHexagonal.Domain.ValueObjects;

namespace BankingHexagonal.Domain.Entities
{
    public class Branch : BaseEntity
    {
        public string BranchName { get; private set; }
        public Address Address { get; private set; }

        private readonly List<Account> _accounts = new();
        public virtual IReadOnlyCollection<Account> Accounts => _accounts.AsReadOnly();

        protected Branch() { }

        public Branch(string branchName, Address address)
        {
            if (string.IsNullOrWhiteSpace(branchName))
                throw new ArgumentException("Şube adı boş olamaz.");

            // Adres null gelemez, gelirse hata fırlatırız
            Address = address ?? throw new ArgumentNullException(nameof(address));

            BranchName = branchName;
            CreatedDate = DateTime.UtcNow;
            Status = DataStatus.Inserted;
        }

        // Şube taşınırsa:
        public void MoveTo(Address newAddress)
        {
            if (newAddress == null) throw new ArgumentNullException(nameof(newAddress));

            Address = newAddress;
            UpdatedDate = DateTime.UtcNow;
            Status = DataStatus.Updated;
        }

        public void UpdateDetails(string newName, Address newAddress)
        {
            if (string.IsNullOrWhiteSpace(newName)) throw new ArgumentException("Şube adı boş olamaz.");

            BranchName = newName;

            // Adres değişimi
            if (newAddress != null && newAddress != Address)
            {
                MoveTo(newAddress);
            }
            else
            {
                // Sadece isim değiştiyse tarihi güncelle
                UpdatedDate = DateTime.UtcNow;
                Status = DataStatus.Updated;
            }
        }
    }
}
