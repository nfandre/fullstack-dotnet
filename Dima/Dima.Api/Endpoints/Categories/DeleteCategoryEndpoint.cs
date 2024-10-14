using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Categories;
using Dima.core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Categories: Delete")
            .WithSummary("Exclui categoria")
            .WithDescription("Exclui categoria")
            .WithOrder(3)
            .Produces<Response<Category?>>();
    
    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user, ICategoryHandler handler, long id)
    {
        var request = new DeleteCategoryRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };
        var result = await handler.DeleteAsync(request);
        
        return result.isSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result.Data);
        // typed results infere o tipo do retorno
    }
}