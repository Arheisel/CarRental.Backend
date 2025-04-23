namespace CarRental.Domain.Entities
{
    public class Car : IEquatable<Car>
    {
        public const int RentalEndDateOffset = 1;
        public const int ServiceDurationDays = 1;

        public required Guid Id { get; set; }

        public required string Type { get; set; }

        public required string Model { get; set; }

        public ICollection<CarService> Services { get; set; } = new List<CarService>();
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

        public bool IsAvailable(DateOnly startDate, DateOnly endDate, Guid? ignoreRental = null)
        {
            IEnumerable<Rental> rentals;
            if (ignoreRental.HasValue) rentals = Rentals.Where(r => r.Id != ignoreRental.Value);
            else rentals = Rentals;

            return !rentals.Any(r => r.Status != Rental.RentalStatus.Canceled && startDate <= r.EndDate.AddDays(RentalEndDateOffset) && endDate.AddDays(RentalEndDateOffset) >= r.StartDate)
                && !Services.Any(s => startDate <= s.Date.AddDays(ServiceDurationDays) && endDate >= s.Date);
        }

        public bool Equals(Car? other)
        {
            return other != null && Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            return obj is Car car && Equals(car);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
