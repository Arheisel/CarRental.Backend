namespace CarRental.Domain.Entities
{
    public class CarService : IEquatable<CarService>
    {
        public required Guid Id { get; set; }

        public required DateOnly Date { get; set; }

        public bool Equals(CarService? other)
        {
            return other != null && Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            return obj is CarService service && Equals(service);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
