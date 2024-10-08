using Dima.Api.Data;
using Dima.Api.Endpoints;
using Dima.Api.Handlers;
using Dima.core.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cnnString =
    builder
        .Configuration
        .GetConnectionString("DefaultConnection") ?? String.Empty;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(cnnString);
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(n => n.FullName));
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

// app.MapPost(
//     "v1/transactions", 
//     (Request model) => new { message = "Hello"}
// );

// app.MapPost("v1/categories",
//         async (CreateCategoryRequest request, ICategoryHandler handler) => await handler.CreateAsync(request)
//     ).WithName("created")
//     .WithSummary("Cria nova categoria")
//     .Produces<Response<Category?>>();
//
// app.MapPut("v1/categories/{id}",
//         async (long id, UpdateCategoryRequest request, ICategoryHandler handler) =>
//         {
//             request.Id = id;
//             await handler.UpdateAsync(request);
//         }
//     ).WithName("update")
//     .WithSummary("atualiza uma categoria")
//     .Produces<Response<Category?>>();
//
// app.MapDelete("v1/categories/{id}",
//         async (long id, ICategoryHandler handler) =>
//         {
//             var request = new DeleteCategoryRequest
//             {
//                 Id = id
//             };
//             return await  handler.DeleteAsync(request);
//         }).WithName("delete")
//     .WithSummary("delete uma categoria")
//     .Produces<Response<Category?>>();
//
// app.MapGet("v1/categories",
//         async ( ICategoryHandler handler) =>
//         {
//             var request = new GetAllCategoriesRequest()
//             {
//                 
//             };
//             return await handler.GetAllAsync(request);
//         }).WithName("get todas categorias")
//     .WithSummary("get todas categorias")
//     .Produces<PagedResponse<List<Category>>?>();
//
// app.MapGet(
//         "v1/categories/{id}",
//         async (long id, ICategoryHandler handler) =>
//         {
//             var request = new GetCagetoryByIdRequest()
//             {
//                 Id = id
//             };
//             return await  handler.GetByIdAsync(request);
//         }).WithName("get uma cagetoria")
//     .WithSummary("get uma categoria")
//     .Produces<Response<Category?>>();

app.MapGet("/", () => new { Message = "Ok" });

app.MapEndpoints();
app.Run();