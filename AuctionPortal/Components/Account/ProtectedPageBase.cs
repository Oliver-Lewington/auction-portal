using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace AuctionPortal.Components.Account;
public class ProtectedPageBase : ComponentBase
{
    [Inject] protected AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;
    [Inject] protected NavigationManager Navigation { get; set; } = default!;

    private bool _shouldRedirect;
    private string? _redirectUrl;

    protected override async Task OnInitializedAsync()
    {
        var state = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = state.User;

        if (!user.Identity?.IsAuthenticated ?? true)
        {
            Snackbar.Add("Please log in to access this page.", Severity.Warning, config =>
            {
                config.RequireInteraction = true;
                config.CloseAfterNavigation = false;
            });

            _shouldRedirect = true;
            _redirectUrl = "/Account/Login";
        }
        else if (!(user.IsInRole("SiteAdmin") || user.IsInRole("AuctionAdmin")))
        {
            Snackbar.Add("You do not have permission to view this page.", Severity.Error, config =>
            {
                config.RequireInteraction = true;
                config.CloseAfterNavigation = false;
            });

            _shouldRedirect = true;
            _redirectUrl = "/";
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && _shouldRedirect && _redirectUrl is not null)
        {
            Navigation.NavigateTo(_redirectUrl, true);
        }
    }

}
