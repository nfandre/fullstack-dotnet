using Dima.Api;
using Dima.Api.Common.Api;
using Dima.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);

app.UseSecurity();

app.MapEndpoints();



app.Run();

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

