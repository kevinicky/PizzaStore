using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source = Pizzas.db";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<PizzaDb>(connectionString);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PizzaStore API", 
        Version = "v1",
        Description = "Making the pizzas you love"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API");
    });
}

app.MapGet("/", () => "hello world");
app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync());
app.MapPost("/pizzas", async (PizzaDb db, Pizza pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizzas/{pizza.Id}", pizza);
});
app.MapGet("/pizzas/{id}", async (PizzaDb db, UInt16 id) => await db.Pizzas.FindAsync(id));
app.MapPut("/pizzas/{id}", async (PizzaDb db, Pizza updatedPizza, UInt16 id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza == null) return Results.NotFound();
    pizza.Name = updatedPizza.Name;
    pizza.Description = updatedPizza.Description;
    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.MapDelete("/pizzas/{id}", async (PizzaDb db, UInt16 id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza == null) return Results.NotFound();
    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();

    return Results.Ok();
});

app.Run();
