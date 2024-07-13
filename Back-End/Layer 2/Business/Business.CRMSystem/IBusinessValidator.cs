namespace Business.CRMSystem;

public interface IBusinessValidator<BusinessType>
{
    bool Validate(BusinessType businessType);
}
