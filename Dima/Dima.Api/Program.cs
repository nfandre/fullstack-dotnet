var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(n => n.FullName));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost(
    "v1/transactions", 
    (Request model) => new { message = "Hello"}
);

app.Run();

public class Request
{
    public string Title { get; set; } = String.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int Type { get; set; }

    public decimal Amount { get; set; }

    public long CategoryId { get; set; }
}