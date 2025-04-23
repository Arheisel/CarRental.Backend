using AutoMapper;
using CarRental.Domain.Entities;

namespace CarRental.API.Application.DTOs
{
    public class CustomerDto
    {
        public required Guid Id { get; set; }

        public required string FullName { get; set; }
    }

    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
