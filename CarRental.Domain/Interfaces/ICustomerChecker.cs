namespace CarRental.Domain.Interfaces
{
    public interface ICustomerChecker
    {
        Task<bool> ExistsAsync(string customerId);
    }
}
