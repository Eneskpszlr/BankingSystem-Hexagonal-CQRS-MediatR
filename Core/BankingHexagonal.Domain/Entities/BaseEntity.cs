using BankingHexagonal.Domain.Enums;
using BankingHexagonal.Domain.Interfaces;

namespace BankingHexagonal.Domain.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public DataStatus Status { get; set; }
    }
}
