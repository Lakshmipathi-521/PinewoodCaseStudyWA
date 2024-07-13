using System.Security.Cryptography;
using Microsoft.Identity.Client;
using Models.Customers;
using ORM.CRMSystem;

namespace DataAccess.CRMSystem;

public class CustomersRepository : ICustomersRepository
{
    private ICustomersContext customersContext;

    public CustomersRepository(ICustomersContext customersContext)
    {
        if (customersContext == default(ICustomersContext))
        {
            throw new ArgumentException("Invalid Customers Context Specified!");
        }

        this.customersContext = customersContext;
    }

    public bool AddEntity(Customer entityType)
    {
        var status = default(bool);
        var validation = entityType != default(Customer);

        if (!validation)
            throw new ArgumentException("Invalid Argument(s) Specified!");

#pragma warning disable CS8604 // Possible null reference argument.
        _ = this.customersContext.Customers.Add(entityType);
#pragma warning restore CS8604 // Possible null reference argument.

        status = this.customersContext.CommitChanges();

        return status;
    }

    public bool DeleteEntity(Customer entityType)
    {
        var status = default(bool);
        var validation = entityType != default(Customer);

        if (!validation)
            throw new ArgumentException("Invalid Argument(s) Specified!");

#pragma warning disable CS8604 // Possible null reference argument.
        _ = this.customersContext.Customers.Remove(entityType);
#pragma warning restore CS8604 // Possible null reference argument.

        status = this.customersContext.CommitChanges();

        return status;
    }

    public void Dispose()
    {
        this.customersContext?.Dispose();
    }

    public IEnumerable<Customer> GetCustomersByName(string customerName)
    {
        var validation = !string.IsNullOrEmpty(customerName);

        if (!validation)
            throw new ArgumentException("Invalid Customer Name Specified!");

        var filteredCustomers = this
            .customersContext.Customers.Where(customer =>
                customer.CustomerName.Contains(customerName)
            )
            .ToList();

        return filteredCustomers;
    }

    public IEnumerable<Customer> GetEntities()
    {
        return this.customersContext.Customers.ToList();
    }

    public Customer GetEntityByKey(int entityKey)
    {
        var validation = entityKey != default(int);

        if (!validation)
            throw new ArgumentException("Invalid Entity Key Specified!");

        var filteredCustomer = this
            .customersContext.Customers.Where(customer => customer.CustomerId.Equals(entityKey))
            .FirstOrDefault();

#pragma warning disable CS8603 // Possible null reference return.
        return filteredCustomer;
#pragma warning restore CS8603 // Possible null reference return.
    }

    public Customer UpdateEntity(Customer entityType)
    {
        var validation = entityType != default(Customer);

        if (!validation)
            throw new ArgumentException("Invalid Entity Information Specified for Updates!");

#pragma warning disable CS8604 // Possible null reference argument.
        var updatedCustomer = this.customersContext.Customers.Update(entityType).Entity;
#pragma warning restore CS8604 // Possible null reference argument.

        _ = this.customersContext.CommitChanges();

        return updatedCustomer;
    }
}
