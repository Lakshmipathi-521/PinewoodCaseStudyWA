using Microsoft.AspNetCore.Mvc;
using Models.Customers;

namespace Controllers.CRMSystem;

public interface ICustomersController : IDisposable
{
    Task<ActionResult<IEnumerable<Customer>>> GetCustomers(int noOfRecords = default(int));
    Task<ActionResult<IEnumerable<Customer>>> SearchCustomerRecords(string? searchString);
    Task<ActionResult<Customer>> GetCustomerDetails(int customerId);
    Task<ActionResult<Customer>> AddNewCustomerRecord(Customer newCustomerRecord);
    Task<ActionResult<Customer>> UpdateExistingCustomerRecord(Customer existingCustomerRecord);
    Task<ActionResult<Customer>> DeleteCustomerRecord(Customer existingCustomerRecord);
}
