using System.Security.Claims;
using Azure;
using Dima.Api.Common.Api;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Transactions;

namespace Dima.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Transaction: Update")
            .WithSummary("Atualiza nova Transaction")
            .WithDescription("Atualiza nova Transaction")
            .WithOrder(2)
            .Produces<Response<Transaction?>>();
    
    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user, ITransactionHandler handler, UpdateTransactionRequest request, long id)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result.Data);
    }
}