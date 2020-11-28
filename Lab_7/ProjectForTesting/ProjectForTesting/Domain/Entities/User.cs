using ProjectForTesting.Domain.ValueObjects;

namespace ProjectForTesting.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; }

        public string Surname { get; set; }

        public AdAccount Account { get; set; }
    }
}