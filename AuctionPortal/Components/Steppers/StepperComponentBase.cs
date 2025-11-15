using AuctionPortal.Components.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;

namespace AuctionPortal.Components.Steppers;

public abstract class StepperComponentBase<TViewModel> : ProtectedPageBase
{
    [Inject] protected new ISnackbar Snackbar { get; set; } = default!;
    [Inject] protected new AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    protected string UserId { get; private set; }
    protected bool IsLoading { get; set; } = true;
    protected StepperValidator<TViewModel>? Validator { get; private set; }

    protected TViewModel ViewModel { get; private set; } = default!;
    public MudStepper? stepper;

    /// <summary>
    /// Indicates whether this stepper is creating a new item or editing an existing one.
    /// False by default (create mode).
    /// </summary>
    protected virtual bool IsEditMode { get; private set; } = false;

    protected override async Task OnInitializedAsync()
    {
        await HandleAuthentication();
    }

    private async Task HandleAuthentication()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                 ?? throw new InvalidOperationException("User ID not found. Authentication is required.");
    }

    /// <summary>
    /// Initializes the ViewModel, either as a new instance or a pre-populated one for editing.
    /// </summary>
    protected void InitializeViewModel(Func<TViewModel> factory, bool isEditMode = false)
    {
        ViewModel = factory();
        IsEditMode = isEditMode;
    }

    /// <summary>
    /// Adds a validator for this stepper's ViewModel.
    /// </summary>
    protected void AddValidation(Action<StepperValidator<TViewModel>> configure)
    {
        if (ViewModel == null)
            throw new ArgumentNullException("ViewModel must be declared before adding validation.");

        Validator = new StepperValidator<TViewModel>(ViewModel);
        configure(Validator);
    }

    /// <summary>
    /// Assigns the MudStepper instance to this base class for use in validation/error handling.
    /// </summary>
    protected void SetStepperReference(MudStepper stepperInstance)
    {
        stepper = stepperInstance;
    }

    /// <summary>
    /// Marks the current active step as having an error.
    /// </summary>
    protected async Task HandleSubmitErrorAsync(Exception ex, string context)
    {
        Console.Error.WriteLine($"Error in {context}: {ex.Message}");

        if (stepper?.ActiveStep is { } activeStep)
        {
            await activeStep.SetHasErrorAsync(true, true);
            await activeStep.SetCompletedAsync(false, true);
        }

        Snackbar.Add($"Something went wrong while {context}. Please try again.", Severity.Error);
    }

    /// <summary>
    /// A general-purpose error handler for non-stepper operations.
    /// </summary>
    protected void HandleError(Exception ex, string context)
    {
        Console.Error.WriteLine($"Error in {context}: {ex.Message}");
        Snackbar.Add($"Something went wrong while {context}. Please try again.", Severity.Error);
    }
}
