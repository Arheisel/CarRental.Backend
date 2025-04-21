namespace CarRental.API.Application.DTOs
{
    public class UpdateRentalDto
    {
        public Guid CarId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
