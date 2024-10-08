using Dima.Api.Common.Api;
using Dima.core;
using Dima.core.Handlers;
using Dima.core.Requests.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Categories: Get All")
            .WithSummary("Consulta todas categoria")
            .WithDescription("Consulta todas categoria")
            .WithOrder(5);
    
    public static async Task<IResult> HandleAsync(
        ICategoryHandler handler, 
        [FromQuery] int pageNumber = Configuration.PageNumber, 
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllCategoriesRequest()
        {
            UserId = "",
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await handler.GetAllAsync(request);
        
        return result.isSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result.Data);
        // typed results infere o tipo do retorno
    }
}