using BankingHexagonal.Domain.Enums;

namespace BankingHexagonal.Domain.Entities.Base
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
