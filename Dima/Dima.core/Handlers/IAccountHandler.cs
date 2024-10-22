using Dima.core.Requests.Account;
using Dima.core.Responses;

namespace Dima.core.Handlers;

public interface IAccountHandler
{
    Task<Response<string>> LoginAsync(LoginRequest request);
    Task<Response<string>> RegisterAsync(RegisterRequest request);
    Task LogoutAsync();
}