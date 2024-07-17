namespace PizzaStore.DB;

public record Pizza
{
    public UInt16 Id { get; init; }
    public string ? Name { get; set; }
}

public class PizzaDb
{
    private static List<Pizza> _pizzas = new()
    {
        new Pizza{ Id = 1, Name = "Pepperoni" },
        new Pizza{ Id = 2, Name = "Chicken" },
        new Pizza{ Id = 3, Name = "Bacon" }
    };

    public static List<Pizza> GetPizzas()
    {
        return _pizzas;
    }

    public static Pizza? GetPizza(UInt16 id)
    {
        return _pizzas.SingleOrDefault(pizza => pizza.Id == id);
    }

    public static Pizza CreatePizza(Pizza pizza)
    {
        _pizzas.Add(pizza);
        return pizza;
    }

    public static Pizza? UpdatePizza(Pizza pizza)
    {
        _pizzas = _pizzas.Select(p =>
        {
            if (pizza.Id == p.Id)
            {
                p.Name = pizza.Name;
            }

            return p;
        }).ToList();

        return null;
    }

    public static void RemovePizza(UInt16 id)
    {
        _pizzas = _pizzas.FindAll(pizza => pizza.Id != id).ToList();
    }
}