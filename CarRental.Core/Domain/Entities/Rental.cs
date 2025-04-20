namespace CarRental.Core.Domain.Entities
{
    public class Rental
    {
        public int Id { get; set; }

        public required string CustomerId { get; set; }
        public required int CarId { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
    }
}
