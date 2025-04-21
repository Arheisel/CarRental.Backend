namespace CarRental.API.Application.DTOs
{
    public class AddRentalDto
    {
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public Guid CarId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
