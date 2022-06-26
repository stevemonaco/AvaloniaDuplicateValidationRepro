using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AvaloniaDuplicateValidationRepro.ViewModels;
public partial class MainWindowViewModel : ObservableValidator
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(6)]
    private string _testNotifyData = "notify";

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [CustomValidation(typeof(MainWindowViewModel), nameof(Validate))]
    private string _customShort = "validate";

    private string _customSetProperty = "setproperty";
    [CustomValidation(typeof(MainWindowViewModel), nameof(Validate))]
    public string CustomSetProperty
    {
        get => _customSetProperty;
        set
        {
            SetProperty(ref _customSetProperty, value, true);
        }
    }

    private string _customFull = "full";
    [CustomValidation(typeof(MainWindowViewModel), nameof(Validate))]
    public string CustomFull
    {
        get => _customFull;
        set
        {
            OnTestNotifyDataChanging(value);
            _testNotifyData = value;
            ValidateProperty(value, nameof(CustomFull));
            SetProperty(ref _customFull, value);
        }
    }

    private string _customValidateBefore = "before";
    [CustomValidation(typeof(MainWindowViewModel), nameof(Validate))]
    public string CustomValidateBefore
    {
        get => _customValidateBefore;
        set
        {
            ValidateProperty(value, nameof(CustomValidateBefore));
            SetProperty(ref _customValidateBefore, value);
        }
    }

    private string _customValidateAfter = "after";
    [CustomValidation(typeof(MainWindowViewModel), nameof(Validate))]
    public string CustomValidateAfter
    {
        get => _customValidateAfter;
        set
        {
            SetProperty(ref _customValidateAfter, value);
            ValidateProperty(value, nameof(CustomValidateAfter));
        }
    }

    public static ValidationResult Validate(string input, ValidationContext context)
    {
        if (input?.Length < 6)
        {
            return ValidationResult.Success!;
        }
        else
        {
            return new ValidationResult("Too Long");
        }
    }
}
