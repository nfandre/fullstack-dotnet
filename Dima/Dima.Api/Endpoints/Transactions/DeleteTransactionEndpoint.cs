using System.Security.Claims;
using Azure;
using Dima.Api.Common.Api;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Transactions;

namespace Dima.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Transaction: Delete")
            .WithSummary("Exclui Transaction")
            .WithDescription("Exclui Transaction")
            .WithOrder(3)
            .Produces<Response<Transaction?>>();
    
    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user, ITransactionHandler handler, long id)
    {
        var request = new DeleteTransactionRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };
        var result = await handler.DeleteAsync(request);
        
        return result.isSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result.Data);
    }
}