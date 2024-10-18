using System.Net.Http.Json;
using System.Text;
using Dima.core.Handlers;
using Dima.core.Requests.Account;
using Dima.core.Responses;

namespace Dima.Web.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    
    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);

        return result.IsSuccessStatusCode
            ? new Response<string>("Login Realizado com sucesso", (int)result.StatusCode, "Login Realizado com sucesso")
            : new Response<string>(null, (int)result.StatusCode, "Não foi possível realizar o login");
    }

    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/identity/register", request);

        return result.IsSuccessStatusCode
            ? new Response<string>("Cadastro realizado com sucesso", (int)result.StatusCode, "Cadastro realizado com sucesso")
            : new Response<string>(null, (int)result.StatusCode, "Não foi possível realizar o cadastro");
    }

    public async Task LogoutAsync()
    {
        var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
        await _client.PostAsJsonAsync("v1/identity/loggout", emptyContent);
    }
}