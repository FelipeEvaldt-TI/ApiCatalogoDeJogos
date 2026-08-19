using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "API do catálogo de jogos está no ar!");

app.MapGet("/api/jogos", () =>
{
    return Results.Ok(new[]
    {
        new{id = 1, titulo = "Hollow Knight", disponivel = true},
        new{id = 2, titulo = "Grand Theft Auto 6", disponivel = false},
    });
});

app.MapGet("/api/jogos/{id:int}", (int id) => 
{
    if (id == 1) return Results.Ok(
        new{id = 1, titulo = "Hollow Knight", disponivel = true});
    if (id == 2) return Results.Ok(
        new{id = 2, titulo = "Grand Theft Auto 6", disponivel = false});
    return Results.NotFound(new {mensagem = "Jogo não encontrado!"});
});

app.MapPost("/api/jogos", async (HttpRequest request) =>
{
    // Lê o corpo da requisição como JSON
    using JsonDocument documento = await JsonDocument.ParseAsync(request.Body);
    string? tituloCriado = documento.RootElement.GetProperty("titulo").GetString();
    return Results.Created("/api/jogos/3", new {id = 3, titulo = tituloCriado, disponivel = true});
});

app.Run();
