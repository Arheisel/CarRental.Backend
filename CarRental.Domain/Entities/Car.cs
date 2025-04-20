namespace CarRental.Domain.Entities
{
    public class Car : IEquatable<Car>
    {
        public required Guid Id { get; set; }

        public required string Type { get; set; }

        public required string Model { get; set; }

        public ICollection<CarService> Services { get; set; } = new List<CarService>();
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

        public bool IsAvailable(DateOnly startDate, DateOnly endDate)
        {
            return Rentals.All(r => r.Status == Rental.RentalStatus.Canceled || r.StartDate > endDate || r.EndDate.AddDays(1) < startDate)
                && Services.All(s => s.Date > endDate || s.Date.AddDays(1) < startDate);
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
