using System.Security.Claims;
using Azure;
using Dima.Api.Common.Api;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Categories;

namespace Dima.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza nova categoria")
            .WithDescription("Atualiza nova categoria")
            .WithOrder(2)
            .Produces<Response<Category?>>();
    
    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user, ICategoryHandler handler, UpdateCategoryRequest request, long id)
    {
        request.Id = id;
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.UpdateAsync(request);
        
        return result.isSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result.Data);
        // typed results infere o tipo do retorno
    }
}