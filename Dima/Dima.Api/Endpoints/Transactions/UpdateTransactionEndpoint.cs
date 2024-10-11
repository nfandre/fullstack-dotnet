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
        ITransactionHandler handler, UpdateTransactionRequest request, long id)
    {
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        
        return result.isSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result.Data);
    }
}