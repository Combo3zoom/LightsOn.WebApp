using System.Text.RegularExpressions;
using LightsOn.WebApp.HttpClients.ApiHttpClient;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Popups;

namespace LightsOn.WebApp.Views.Components.CallButtonContainer;

[Serializable]
public class Customer
{
    public string Name { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    
    public bool IsNameValid { get; set; } = false;
    public bool IsPhoneNumberValid { get; set; } = false;
    
}
public partial class CallButtonContainer: ComponentBase
{
    private bool _showDialog;
    [SupplyParameterFromForm] 
    private Customer Customer { get; set; }
    
    private CancellationTokenSource _validationCts;
    private string _nameValidationMessage;
    private string _phoneValidationMessage;

    private SfTooltip _tooltip;
    private string _tooltipContent;
    
    [Inject]
    private IApiHttpClient ApiClient { get; set; }
    public CallButtonContainer()
    {
        _showDialog = false;
        Customer = new Customer();
        
        _validationCts = new CancellationTokenSource();
        _nameValidationMessage = null!;
        _phoneValidationMessage = null!;
        
        _tooltip = null!;
        
        _tooltipContent = "Перевірте введені дані";
    }

    private async Task OpenDialog()
    {
        _showDialog = true;
        await InvokeAsync(StateHasChanged); 
    }
    private async Task SubmitCallRequest()
    {
        if (Customer is { IsNameValid: true, IsPhoneNumberValid: true })
        {
            _showDialog = false;
            var response = await ApiClient.SubmitCustomerAsync(Customer);
            ShowTooltip(response.IsSuccessStatusCode
                ? "Ваш запит успішно відправлено!"
                : "Сталася помилка під час відправлення запиту.");
            await InvokeAsync(StateHasChanged);
        }
    }
    
    private async Task ValidateName(ChangeEventArgs e)
    {
        Customer.Name = e.Value!.ToString()!;
        
        await WaitingTime(500);
        
        var nameRegex = new Regex(@"^[a-zA-ZА-Яа-яЄєІіЇїҐґ'ь]+$");
        
        if (string.IsNullOrWhiteSpace(Customer.Name)) _nameValidationMessage = "Будь ласка, введіть ваше ім'я.";
        else if (!nameRegex.IsMatch(Customer.Name)) _nameValidationMessage = "Ім'я може містити тільки літери та цифри.";
        else _nameValidationMessage = string.Empty;
        
        Customer.IsNameValid = string.IsNullOrEmpty(_nameValidationMessage);
    }
    
    private async Task ValidatePhoneNumber(ChangeEventArgs e)
    {
        Customer.PhoneNumber = e.Value?.ToString() ?? "";

        await WaitingTime(500);
            
        var phoneRegex = new Regex(@"^(\+?380|0)?(\s|-)?\d{2}(\s|-)?\d{3}(\s|-)?\d{2}(\s|-)?\d{2}$");
        
        if (string.IsNullOrWhiteSpace(Customer.PhoneNumber)) _phoneValidationMessage = "Будь ласка, введіть ваш номер телефону.";
        else if (!phoneRegex.IsMatch(Customer.PhoneNumber)) _phoneValidationMessage = "Невірний формат номера телефону.";
        else _phoneValidationMessage = string.Empty;
        
        Customer.IsPhoneNumberValid = string.IsNullOrEmpty(_phoneValidationMessage);
    }

    private async Task WaitingTime(int timeDelay)
    {
        await _validationCts.CancelAsync();
        _validationCts.Dispose();
        _validationCts = new CancellationTokenSource();

        await Task.Delay(timeDelay, _validationCts.Token);
    }
    
    private void ShowTooltip(string message)
    {
        _tooltipContent = message;
        _tooltip.Open();
        StateHasChanged();

        Task.Delay(3000).ContinueWith(t =>
        {
            _tooltip.Close();
            InvokeAsync(StateHasChanged);
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
}