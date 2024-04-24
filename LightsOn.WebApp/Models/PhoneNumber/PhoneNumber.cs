namespace LightsOn.WebApp.Models.PhoneNumber;

public class CompanyPhoneNumber(string phoneNumber)
{
    public string PhoneNumber { get; set; } = phoneNumber;

    private CompanyPhoneNumber() : this(string.Empty)
    {
    }
}