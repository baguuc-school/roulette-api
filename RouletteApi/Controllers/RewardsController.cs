using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouletteApi.Context;
using RouletteApi.Models;
using System.Linq;

namespace RouletteApi.Controllers
{
    public class RewardsController : Controller
    {
        private readonly DatabaseContext database;

        public RewardsController(DatabaseContext context)
        {
            database = context;
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
        public async Task<IResult> Index([FromBody] RecordedReward reward)
        {
            database.Add(reward);
            await database.SaveChangesAsync();

            return Results.Created();
        }
    }
}
