using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Categories;
using Dima.core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Cria nova categoria")
            .WithDescription("Cria nova categoria")
            .WithOrder(1)
            .Produces<Response<Category?>>(); // Com typed results não é necessário

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,ICategoryHandler handler, CreateCategoryRequest request)
    {
        request.UserId = user.Identity?.Name ?? String.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess 
            ? TypedResults.Created($"{result.Data?.Id}", result) 
            : TypedResults.BadRequest(result.Data);
        // typed results infere o tipo do retorno
    }
}