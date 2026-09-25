using BlazingPizza;
using BlazingPizza.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Controllers;

[Route("specials")]
[ApiController]
public class SpecialsController : ControllerBase
{
    private readonly PizzaStoreContext db;

    public SpecialsController(PizzaStoreContext db)
    {
        this.db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<PizzaSpecial>>> GetSpecials()
    {
        return (await db.Specials.ToListAsync())
            .OrderByDescending(special => special.BasePrice)
            .ToList();
    }
}