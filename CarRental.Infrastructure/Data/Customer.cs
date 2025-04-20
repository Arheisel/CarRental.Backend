using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data
{
    public class Customer : BaseRecord
    {
        public required string CustomerId { get; set; }

        public required string FullName { get; set; }

        public required string Address { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; } = new HashSet<Rental>();
    }

    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.CustomerId)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(c => c.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Address)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(c => c.CustomerId)
                .IsUnique();
        }
    }
}
