namespace BlazingPizza.Data;

public class PizzaService
{
    public Task<Pizza[]> GetPizzasAsync()
    {
    // Call your data access technology here
        return Task.FromResult(new Pizza[]
        {
            new Pizza
            {
                PizzaId = 1,
                Name = "Margherita",
                Description = "Classic tomato and mozzarella",
                Price = 9.99M,
                Vegetarian = true,
                Vegan = false
            },
            new Pizza
            {
                PizzaId = 2,
                Name = "Pepperoni",
                Description = "Pepperoni and cheese",
                Price = 12.99M,
                Vegetarian = false,
                Vegan = false
            }
        });
    }
}