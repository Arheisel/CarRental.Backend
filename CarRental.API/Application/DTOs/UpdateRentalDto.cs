namespace CarRental.API.Application.DTOs
{
    public class UpdateRentalDto
    {
        public Guid CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
