using RouletteApi.Utils;

namespace RouletteApi.Models
{
    public class Item : IWeightedItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int RouletteId { get; set; }
    }
}
