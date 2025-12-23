namespace RouletteApi.Utils
{
    public class Utils
    {
        public static T WeightedPick<T>(IEnumerable<T> seq, Random random) where T : IWeightedItem
        {
            T[] pool = seq.OrderBy(i => i.Value).ToArray();
            int max = pool[pool.Length - 1].Value;

            Dictionary<int, T> d = new Dictionary<int, T>();

            // invert the weights so that higher = smaller so higher = less common
            for (int i = 0; i < pool.Length; i++)
            {
                int weight = max - pool[i].Value;
                d.Add(weight, pool[i]);
            }

            List<int> keys = d.Keys.ToList();

            // Calculate weights using exponential function
            double[] weights = new double[pool.Length];
            double totalWeight = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                // Using value squared as weight (or other exponential)
                weights[i] = Math.Pow(keys[i], 2);
                totalWeight += weights[i];
            }

            // Generate random value
            double randomValue = random.NextDouble() * totalWeight;

            // Find which item this corresponds to
            double cumulativeWeight = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                cumulativeWeight += weights[i];
                if (randomValue < cumulativeWeight)
                {
                    return d[keys[i]];
                }
            }

            return d[keys[^1]];
        }
    }
}
