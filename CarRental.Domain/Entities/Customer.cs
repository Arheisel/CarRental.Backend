namespace CarRental.Domain.Entities
{
    public class Customer : IEquatable<Customer>
    {
        public required Guid Id { get; set; }

        public required string CustomerId { get; set; }

        public required string FullName { get; set; }

        public required string Address { get; set; }

        public bool Equals(Customer? other)
        {
            return other != null && Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            return obj is Customer customer && Equals(customer);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
