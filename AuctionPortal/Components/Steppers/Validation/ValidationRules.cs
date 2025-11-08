using AuctionPortal.ViewModels;

namespace AuctionPortal.Components.Steppers.Validation;

public static class ValidationRules
{
    /// <summary>
    /// Provides validation rules for the Auction stepper (create/edit).
    /// </summary>
    public static Action<StepperValidator<AuctionViewModel>> GetAuctionValidationRules() => v =>
    {
        // Step 1: Basic info
        v.AddRule(0, a => !string.IsNullOrWhiteSpace(a.Name), "Auction name is required.");

        // Step 2: Image
        v.AddRule(1, a => a.Image != null, "An image is required.");

        // Step 3: Timing
        v.AddRule(2, a => a.BeginsAt.Date >= DateTime.Today.AddDays(1), "Auction must start on or after tomorrow.");
        v.AddRule(2, a => a.EndsAt > a.BeginsAt,  "End time must be after the start time.");
    };

    /// <summary>
    /// Provides validation rules for the Product stepper (create/edit).
    /// </summary>
    public static Action<StepperValidator<ProductViewModel>> GetProductValidationRules() => v =>
    {
        // Step 1: Basic info
        v.AddRule(0, p => p.Images != null && p.Images.Any(), "Please upload at least one image.");

        // Step 2: Image
        v.AddRule(1, p => !string.IsNullOrWhiteSpace(p.Title), "Product name is required.");

        // Step 3: Pricing and expiry
        v.AddRule(2, p => p.StartingPrice > 0, "Starting price must be greater than 0.");
        v.AddRule(2, p => p.ExpiryDate == default || p.ExpiryDate > DateTime.Now, "Expiry date must be in the future.");
    };
}