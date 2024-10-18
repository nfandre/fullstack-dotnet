using Dima.core.Handlers;
using Dima.core.Requests.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class RegisterPage : ComponentBase
{
    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    
    [Inject]
    public IAccountHandler Handler { get; set; } = null!;
    
    [Inject]
    public NavigationManager NavigationManger { get; set; } = null!;
    
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    #endregion

    #region Properties
    public RegisterRequest InputModel { get; set; } = new();
    public bool IsBusy { get; set; } = false;
    #endregion
    
    #region overrides
    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        
        if(user.Identity is { IsAuthenticated: true})
            NavigationManger.NavigateTo("/");
    }
    #endregion

    #region Methods
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.RegisterAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message, Severity.Success);
                NavigationManger.NavigateTo("login");
            }
            else
                Snackbar.Add(result.Message, Severity.Error);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Snackbar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion

}
