using AutoMapper;
using CarRental.Domain.Entities;

namespace CarRental.API.Application.DTOs
{
    public class CarDto
    {
        public required Guid Id { get; set; }

        public required string Type { get; set; }

        public required string Model { get; set; }
    }
    public class CarDtoProfile : Profile
    {
        public CarDtoProfile()
        {
            CreateMap<Car, CarDto>();
        }
    }
}
