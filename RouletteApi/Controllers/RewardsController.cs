using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouletteApi.Context;
using RouletteApi.Models;
using RouletteApi.Utils;
using System.Linq;

namespace RouletteApi.Controllers
{
    public class RewardsController : Controller
    {
        private readonly DatabaseContext database;
        private readonly Random random;

        public RewardsController(DatabaseContext context)
        {
            database = context;
            random = new Random();
        }

        public async Task<IResult> TopUsers()
        {
            List<UserRewardScore> scores = database.Set<RecordedReward>().GroupBy(record => record.Username)
                .Select(r => new UserRewardScore
                {
                    Username = r.Key,
                    TotalScore = r.Sum(r => r.Item.Value)
                })
                .Take(5)
                .OrderByDescending(record => record.TotalScore)
                .ToList();

            return Results.Ok(scores);
        }

        [HttpPost]
        public async Task<IResult> Index([FromBody] RollStartData data)
        {
            var roulette = database.Set<Roulette>()
                .Include(r => r.Items)
                .Where(r => r.Id == data.RouletteId)
                .FirstOrDefault();


            var itemId = Utils.Utils.WeightedPick(roulette.Items, random).Id;
            var item = database.Set<Item>()
                .Where(i => i.Id == itemId)
                .FirstOrDefault();

            RecordedReward reward = new RecordedReward
            {
                Timestamp = DateTime.UtcNow,
                Username = data.Username,
                ItemId = item.Id
            };

            database.Add(reward);
            await database.SaveChangesAsync();


            return Results.Ok(reward);
        }
    }
}
