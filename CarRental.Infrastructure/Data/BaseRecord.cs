namespace CarRental.Infrastructure.Data
{
    public abstract class BaseRecord
    {
        public Guid Id { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
