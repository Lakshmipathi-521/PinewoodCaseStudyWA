using Models.Customers;

namespace DataAccess.CRMSystem;

public interface ICustomersRepository : IRepository<Customer, int>
{
    IEnumerable<Customer> GetCustomersByName(string customerName);
}
