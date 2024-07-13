using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Customers;

namespace ORM.CRMSystem;

public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        _ = builder.HasKey(model => model.CustomerId);
        _ = builder.Property(model => model.CustomerId).UseIdentityColumn();
        _ = builder.ToTable("Customers");
    }
}
