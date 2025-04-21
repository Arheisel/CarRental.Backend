namespace CarRental.Domain.Entities
{
    public class Rental : IEquatable<Rental>
    {
        public required Guid Id { get; set; }

        public required Customer Customer { get; set; }

        public required DateOnly StartDate { get; set; }

        public required DateOnly EndDate { get; set; }

        public required Car Car { get; set; }

        public RentalStatus Status { get; set; }

        public enum RentalStatus
        {
            None = 0,
            Valid = 1,
            Canceled = 2
            //Other things could go here such as Pending, CheckedOut, Returned, Completed, etc.
        }

        public bool Equals(Rental? other)
        {
            return other != null && Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            return obj is Rental rental && Equals(rental);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
