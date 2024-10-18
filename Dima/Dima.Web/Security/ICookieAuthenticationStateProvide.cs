using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Web.Security;

public interface ICookieAuthenticationStateProvide
{
    Task<bool> CheckAuthenticatedAsync();
    Task<AuthenticationState> GetAuthenticationStateAsync();
    void NotifyAuthenticationStateChanged();
}