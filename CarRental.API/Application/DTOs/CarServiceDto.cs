namespace CarRental.API.Application.DTOs
{
    public class CarServiceDto
    {
        public required CarDto Car { get; set; }

        public required DateOnly Date { get; set; }
    }
}
