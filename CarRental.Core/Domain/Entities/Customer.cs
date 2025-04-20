namespace CarRental.Core.Domain.Entities
{
    public class Customer
    {
        public required string Id { get; set; }

        public required string FullName { get; set; }

        public required string Address { get; set; }
    }
}
