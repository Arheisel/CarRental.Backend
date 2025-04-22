using AutoMapper;
using CarRental.Domain.Entities;

namespace CarRental.API.Application.DTOs
{
    public class RentalDto
    {
        public required Guid Id { get; set; }

        public required DateOnly StartDate { get; set; }

        public required DateOnly EndDate { get; set; }

        public required CarDto Car { get; set; }

        public required string Status { get; set; }
    }

    public class RentalDtoProfile : Profile
    {
        public RentalDtoProfile()
        {
            CreateMap<Rental, RentalDto>()
                .ForMember(r => r.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(Rental.RentalStatus), src.Status)));
        }
    }
}
