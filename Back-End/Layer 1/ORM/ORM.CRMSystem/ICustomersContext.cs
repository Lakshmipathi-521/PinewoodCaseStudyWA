using Microsoft.EntityFrameworkCore;
using Models.Customers;

namespace ORM.CRMSystem;

public interface ICustomersContext : ISystemContext
{
    DbSet<Customer> Customers { get; set; }
}
