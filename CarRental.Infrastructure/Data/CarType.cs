using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Data
{
    public class CarType : BaseRecord
    {
        public required string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; } = new HashSet<Car>();
    }

    internal class CarTypeConfiguration : IEntityTypeConfiguration<CarType>
    {
        public void Configure(EntityTypeBuilder<CarType> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
