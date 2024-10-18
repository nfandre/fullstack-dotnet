using System.Net.Http.Json;
using System.Security.Claims;
using Dima.core.Models.Account;
using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Web.Security;

public class CookieAuthenticationStateProvide(IHttpClientFactory clientFactory) :
    AuthenticationStateProvider,
    ICookieAuthenticationStateProvide
{
    private bool _isAuthenticated = false;
    private readonly HttpClient _client = clientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<bool> CheckAuthenticatedAsync()
    {
        await GetAuthenticationStateAsync();
        return _isAuthenticated;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _isAuthenticated = false;
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        var userInfo = await GetUser();
        if(userInfo is null)
            return new AuthenticationState(user);

        var claims = await GetClaims(userInfo);

        var id = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvide));
        user = new ClaimsPrincipal(id);
        
        _isAuthenticated = true;
        return new AuthenticationState(user);
    }

    public void NotifyAuthenticationStateChanged() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());


    private async Task<User?> GetUser()
    {
        try
        {
            return await _client.GetFromJsonAsync<User?>("v1/identity/manage/info");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    private async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Email, user.Email),
        };
        claims.AddRange(
            user.Claims.Where(x =>
                x.Key != ClaimTypes.Name &&
                x.Key != ClaimTypes.Email
                )
                .Select(c 
                    => new Claim(c.Key, c.Value)
                )
            );

        RoleClaim[]? roles;

        try
        {
            roles = await _client.GetFromJsonAsync<RoleClaim[]>("v1/identity/roles");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return claims;
        }
        // foreach (var role in roles ?? [])
        // {
        //     // User.IsInRole
        //     if(!string.IsNullOrEmpty(role.Type) && !string.IsNullOrEmpty(role.Value))
        //         claims.Add(new Claim(role.Type, role.Value, role.ValueType, role.Issuer, role.OriginalIssuer));
        // }
        claims.AddRange(
            from role in roles ?? [] 
            where !string.IsNullOrEmpty(role.Type) && !string.IsNullOrEmpty(role.Value) 
            select new Claim(role.Type, role.Value, role.ValueType, role.Issuer, role.OriginalIssuer)
            );
        return claims;
    }
}