namespace Business.CRMSystem;

public class SearchStringValidator : IBusinessValidator<string>
{
    private const int MIN_SEARCH_LENGTH = 3;
    private string[] badKeywords = { "bad", "worse", "awful", "not good" };

    public bool Validate(string businessType)
    {
        var validation =
            !badKeywords.Contains(businessType) && businessType.Length >= MIN_SEARCH_LENGTH;

        return validation;
    }
}
