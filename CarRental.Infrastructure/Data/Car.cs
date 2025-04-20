using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data
{
    public class Car : BaseRecord
    {
        public required Guid TypeId { get; set; }

        public required string Model { get; set; }

        public virtual CarType? Type { get; set; }

        public virtual ICollection<CarService> Services { get; set; } = new HashSet<CarService>();

        public virtual ICollection<Rental> Rentals { get; set; } = new HashSet<Rental>();
    }

    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasOne(c => c.Type)
                .WithMany(t => t.Cars)
                .HasForeignKey(c => c.TypeId)
                .IsRequired();

            builder.Property(c => c.Model)
                .HasMaxLength(50)
                .IsRequired();
        }
    }

    internal class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Domain.Entities.Car, Car>()
                .AfterMap((src, dest) =>
                {
                    foreach (var service in dest.Services) service.CarId = dest.Id;
                });

            CreateMap<Car, Domain.Entities.Car>()
                .ForMember(c => c.Type, opt => opt.MapFrom(src => src.Type!.Name));
        }
    }
}
