using Microsoft.EntityFrameworkCore;

namespace RouletteApi.Models
{
    public class Roulette
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; } = [];
    }
}
