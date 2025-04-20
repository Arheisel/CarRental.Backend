namespace CarRental.API.Application.DTOs
{
    public class ErrorDto
    {
        public required string Instance { get; set; }

        public required int Status { get; set; }

        public required string Title { get; set; }

        public required string Detail { get; set; }

        public required string Path { get; set; }
    }
}
