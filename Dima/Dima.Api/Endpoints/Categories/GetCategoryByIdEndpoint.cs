using Azure;
using Dima.Api.Common.Api;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Categories;

namespace Dima.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Consulta categoria")
            .WithDescription("Consulta categoria")
            .WithOrder(4)
            .Produces<Response<Category?>>();
    
    public static async Task<IResult> HandleAsync(
        ICategoryHandler handler, long id)
    {
        var request = new GetCagetoryByIdRequest
        {
            Id = id,
            UserId = "teste@teste.com"
        };
        var result = await handler.GetByIdAsync(request);
        
        return result.isSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result.Data);
        // typed results infere o tipo do retorno
    }
}