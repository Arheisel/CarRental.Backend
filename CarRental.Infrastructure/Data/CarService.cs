using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Infrastructure.Data
{
    public class CarService : BaseRecord
    {
        public Guid CarId { get; set; }

        public DateOnly Date { get; set; }

        public virtual Car? Car { get; set; }
    }

    internal class CarServiceConfiguration : IEntityTypeConfiguration<CarService>
    {
        public void Configure(EntityTypeBuilder<CarService> builder)
        {
            builder.Property(s => s.Date)
                .IsRequired();

            builder.HasOne(s => s.Car)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.CarId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
