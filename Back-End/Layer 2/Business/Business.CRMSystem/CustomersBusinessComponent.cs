using DataAccess.CRMSystem;
using Models.Customers;

namespace Business.CRMSystem;

public class CustomersBusinessComponent : ICustomersBusinessComponent
{
    private ICustomersRepository customersRepository;
    private IBusinessValidator<string> searchStringValidator;
    private IBusinessValidator<Customer> customerValidator;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public CustomersBusinessComponent(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        ICustomersRepository customersRepository,
        IBusinessValidator<string> searchStringValidator,
        IBusinessValidator<Customer> customerValidator
    )
    {
        var validation =
            customersRepository != null
            && searchStringValidator != null
            && customerValidator != null;

        if (!validation)
            throw new ArgumentException("Invalid Dependencies Provided!");

#pragma warning disable CS8601 // Possible null reference assignment.
        this.customersRepository = customersRepository;
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
        this.searchStringValidator = searchStringValidator;
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
        this.customerValidator = customerValidator;
#pragma warning restore CS8601 // Possible null reference assignment.
    }

    public bool AddNewCustomer(Customer customer)
    {
        var validation =
            customer != null
            && this.customersRepository != null
            && this.customerValidator.Validate(customer);

        if (!validation)
            throw new ArgumentException("Invalid Customer Details Provided for Save!");

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var status = customersRepository.AddEntity(customer);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.

        return status;
    }

    public bool DeleteCustomer(Customer customer)
    {
        var validation = customer != null && this.customersRepository != null;

        if (!validation)
            throw new ArgumentException("Invalid Customer Details Provided for Save!");

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var status = customersRepository.DeleteEntity(customer);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.

        return status;
    }

    public void Dispose()
    {
        customersRepository?.Dispose();
    }

    public Customer GetCustomerDetails(int customerId)
    {
        var validation =
            customerId != default(int) && this.customersRepository != default(ICustomersRepository);

        if (!validation)
            throw new ArgumentException("Invalid Customer Id or Dependencies Provided!");

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var filteredCustomer = customersRepository.GetEntityByKey(customerId);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return filteredCustomer;
    }

    public IEnumerable<Customer> GetCustomers(string? customerName = null)
    {
        var filteredCustomers = default(IEnumerable<Customer>);

        if (string.IsNullOrEmpty(customerName))
            filteredCustomers = customersRepository.GetEntities();
        else
        {
            var validation = searchStringValidator.Validate(customerName);

            if (!validation)
                throw new ArgumentException("Invalid Customer Name Specified for Search!");

            filteredCustomers = customersRepository.GetCustomersByName(customerName);
        }

        return filteredCustomers;
    }

    public Customer UpdateCustomer(Customer existingCustomer)
    {
        var validation =
            existingCustomer != default(Customer)
            && customersRepository != default(ICustomersRepository);

        if (!validation)
            throw new ArgumentException(
                "Invalid Customer Details or Dependencies Provided for Updates!"
            );

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var updatedCustomer = customersRepository.UpdateEntity(existingCustomer);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.

        return updatedCustomer;
    }
}
