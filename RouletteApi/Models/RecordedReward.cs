namespace RouletteApi.Models
{
    public class RecordedReward
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Username { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
    }
}
