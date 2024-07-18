using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models;

public class Pizza
{
    public UInt16 Id { get; init; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class PizzaDb : DbContext
{
    public PizzaDb(DbContextOptions options) : base(options) { }
    public DbSet<Pizza> Pizzas { get; init; } = null!;
}