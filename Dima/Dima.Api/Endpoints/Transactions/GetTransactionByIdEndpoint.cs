using Dima.Api.Common.Api;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Transactions;
using Dima.core.Responses;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Transaction: Get By Id")
            .WithSummary("Consulta Transaction")
            .WithDescription("Consulta Transaction")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();

    public static async Task<IResult> HandleAsync(
        ITransactionHandler handler, long id)
    {
        var request = new GetTransactionByIdRequest()
        {
            Id = id,
            UserId = "teste@teste.com"
        };
        var result = await handler.GetByIdAsync(request);

        return result.isSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result.Data);
    }
}