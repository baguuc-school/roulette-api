using Microsoft.AspNetCore.Mvc;
using RouletteApi.Context;
using RouletteApi.Models;

namespace RouletteApi.Controllers
{
    public class RoulettesController : Controller
    {
        private readonly DatabaseContext database;

        public RoulettesController(DatabaseContext context)
        {
            database = context;
        }

        public IResult Index()
        {
            var records = from roulette in database.Set<Roulette>()
                         select new
                         {
                             Id = roulette.Id,
                             Name = roulette.Name
                         };

            return Results.Ok(records);
        }

        public IResult Details(int id)
        {
            var records = from roulettes in database.Set<Roulette>()
                         join items in database.Set<Item>()
                         on roulettes.Id equals items.RouletteId into grouping
                         where roulettes.Id == id
                         select new
                         {
                             Id = roulettes.Id,
                             Name = roulettes.Name,
                             Items = grouping
                         };

            if(records.Count() > 0)
            {
                return Results.Ok(records.First());
            } else
            {
                return Results.NotFound();
            }
        }

        [HttpPost]
        public async Task<IResult> Index([FromBody] Roulette roulette)
        {
            database.Add(roulette);
            await database.SaveChangesAsync();

            return Results.Created();
        }
    }
}
