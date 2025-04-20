using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data
{
    public class Rental : BaseRecord
    {
        public Guid CustomerId { get; set; }

        public Guid CarId { get; set; }

        public required DateOnly StartDate { get; set; }

        public required DateOnly EndDate { get; set; }

        public required string Status { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual Car? Car { get; set; }
    }

    internal class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(r => r.Car)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }

    internal class RentalProfile : Profile
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
