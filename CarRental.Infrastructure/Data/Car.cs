using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data
{
    public class Car : BaseRecord
    {
        public required string Type { get; set; }

        public required string Model { get; set; }

        public virtual ICollection<CarService> Services { get; set; } = new HashSet<CarService>();

        public virtual ICollection<Rental> Rentals { get; set; } = new HashSet<Rental>();
    }

    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.Property(c => c.Type)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Model)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
