using AutoMapper;
using CarRental.Infrastructure.Data;
using CarRental.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Repositories
{
    public class RentalRepository(AppDbContext context, IMapper mapper) : BaseRepository<Domain.Entities.Rental, Rental>(context, mapper), IRentalRepository
    {
        public async Task<Domain.Entities.Rental?> GetAsync(Guid id)
        {
            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Car)
                    .ThenInclude(c => c!.Rentals)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (rental == null) return null;

            return _mapper.Map<Domain.Entities.Rental>(rental);
        }
    }

    public class RentalProfile : Profile
    {
        public RentalProfile()
        {
            CreateMap<Domain.Entities.Rental, Rental>()
                .ForMember(r => r.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
                .ForMember(r => r.CarId, opt => opt.MapFrom(src => src.Car.Id))
                .ForMember(r => r.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(Domain.Entities.Rental.RentalStatus), src.Status)));

            CreateMap<Rental, Domain.Entities.Rental>()
                .ForMember(r => r.Status, opt => opt.MapFrom(src => Enum.Parse(typeof(Domain.Entities.Rental.RentalStatus), src.Status)));
        }
    }
}
