using System.Security.Claims;
using Azure;
using Dima.Api.Common.Api;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Transactions;

namespace Dima.Api.Endpoints.Transactions;

public class CreateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Transação: Create")
            .WithSummary("Cria nova Transação")
            .WithDescription("Cria nova Transação")
            .WithOrder(1)
            .Produces<Response<Transaction?>>(); // Com typed results não é necessário

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user, ITransactionHandler handler, CreateTransactionRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.isSuccess 
            ? TypedResults.Created($"{result.Data?.Id}", result) 
            : TypedResults.BadRequest(result.Data);
    }
    
}