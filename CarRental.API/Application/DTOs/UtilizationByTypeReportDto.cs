namespace CarRental.API.Application.DTOs
{
    public class UtilizationByTypeReportDto
    {
        public required string Type { get; set; }
        public required double Percentage { get; set; }
    }
}
