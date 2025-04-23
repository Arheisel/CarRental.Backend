
using CarRental.API.Application.Interfaces;
using CarRental.API.Application.Services;
using CarRental.Domain.Interfaces;
using CarRental.Domain.Services;
using CarRental.Infrastructure;
using CarRental.Infrastructure.Interfaces;
using CarRental.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UADE.Extensions.Middleware;

namespace CarRental.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services);

            var app = builder.Build();

            ConfigureApp(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("DataSource=car_rental.db"), ServiceLifetime.Transient);

            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<IReportsService, ReportsService>();

            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            
            services.AddScoped<ICustomerChecker, CustomerRepository>();
            services.AddScoped<IRentalSystem, RentalSystem>();

            services.AddAutoMapper(typeof(Program).Assembly, typeof(AppDbContext).Assembly);
        }

        private static void ConfigureApp(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors();

            // app.UseAuthorization();

            app.UseExceptionsMiddleware();

            app.MapControllers();
        }
    }
}
