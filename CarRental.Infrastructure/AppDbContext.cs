using CarRental.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<CarType> CarTypes { get; set; }

        public DbSet<CarService> CarServices { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSeeding((context, _) => SeedDataAsync(context, _, CancellationToken.None).Wait())
                .UseAsyncSeeding(SeedDataAsync);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        private async Task SeedDataAsync(DbContext context, bool _, CancellationToken cancellationToken)
        {
            var carTypes = context.Set<CarType>();

            var sedan = new CarType { Id = Guid.NewGuid(), Name = "Sedan" };
            carTypes.Add(sedan);

            var hatchback = new CarType { Id = Guid.NewGuid(), Name = "Hatchback" };
            carTypes.Add(hatchback);

            var suv = new CarType { Id = Guid.NewGuid(), Name = "SUV" };
            carTypes.Add(suv);

            var cars = context.Set<Car>();

            cars.Add(new Car { 
                Id = Guid.NewGuid(), 
                Model = "Toyota Corolla", 
                TypeId = sedan.Id, 
                DateAdded = DateTime.UtcNow,
                Services =
                [
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 5, 1) },
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 7, 1) },
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 9, 1) },
                ] 
            });

            cars.Add(new Car
            {
                Id = Guid.NewGuid(),
                Model = "Honda Accord",
                TypeId = sedan.Id,
                DateAdded = DateTime.UtcNow,
                Services =
                [
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 5, 3) },
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 7, 3) },
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 9, 3) },
                ]
            });

            cars.Add(new Car
            {
                Id = Guid.NewGuid(),
                Model = "Nissan Altima",
                TypeId = sedan.Id,
                DateAdded = DateTime.UtcNow,
                Services =
                [
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 5, 5) },
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 7, 5) },
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 9, 5) },
                ]
            });

            cars.Add(new Car
            {
                Id = Guid.NewGuid(),
                Model = "Volkswagen Golf",
                TypeId = hatchback.Id,
                DateAdded = DateTime.UtcNow,
                Services =
                [
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 5, 7) },
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 7, 7) },
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 9, 7) },
                ]
            });

            cars.Add(new Car
            {
                Id = Guid.NewGuid(),
                Model = "Ford Focus",
                TypeId = hatchback.Id,
                DateAdded = DateTime.UtcNow,
                Services =
                [
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 5, 9) },
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 7, 9) },
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 9, 9) },
                ]
            });

            cars.Add(new Car
            {
                Id = Guid.NewGuid(),
                Model = "Honda CR-V",
                TypeId = suv.Id,
                DateAdded = DateTime.UtcNow,
                Services =
                [
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 5, 11) },
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 7, 11) },
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 9, 11) },
                ]
            });

            cars.Add(new Car
            {
                Id = Guid.NewGuid(),
                Model = "Toyota RAV4",
                TypeId = suv.Id,
                DateAdded = DateTime.UtcNow,
                Services =
                [
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 5, 13) },
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 7, 13) },
                    new() { Id = Guid.NewGuid(), DateAdded = DateTime.UtcNow, Date = new DateOnly(2025, 9, 13) },
                ]
            });

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
