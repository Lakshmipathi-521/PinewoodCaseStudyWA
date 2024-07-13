using Models.Customers;

namespace Business.CRMSystem;

public class CustomerEntityValidator : IBusinessValidator<Customer>
{
    private const int MIN_CREDIT_LIMIT = 1;

    public bool Validate(Customer businessType)
    {
        var validation =
            businessType != default(Customer)
            && !string.IsNullOrEmpty(businessType.CustomerName)
            && businessType.CreditLimit >= MIN_CREDIT_LIMIT
            && businessType.IsActive;

        return validation;
    }
}
