using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouletteApi.Context;
using RouletteApi.Models;

namespace RouletteApi.Controllers
{
    public class RewardsController : Controller
    {
        private readonly DatabaseContext database;

        public RewardsController(DatabaseContext context)
        {
            database = context;
        }

        public async Task<IResult> Top()
        {
            var rewards = await database.Rewards
                .Include(reward => reward.Item)
                .OrderByDescending(reward => reward.Item.Value)
                .Take(5)
                .ToListAsync();

            return Results.Ok(rewards);
        }

        [HttpPost]
        public async Task<IResult> Index([FromBody] RecordedReward reward)
        {
            database.Add(reward);
            await database.SaveChangesAsync();

            return Results.Created();
        }
    }
}
