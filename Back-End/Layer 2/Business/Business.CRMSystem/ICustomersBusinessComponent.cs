using Models.Customers;

namespace Business.CRMSystem;

public interface ICustomersBusinessComponent : IDisposable
{
    IEnumerable<Customer> GetCustomers(string? customerName = default);
    Customer GetCustomerDetails(int customerId);
    bool AddNewCustomer(Customer customer);
    Customer UpdateCustomer(Customer existingCustomer);
    bool DeleteCustomer(Customer customer);
}
