using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetDomain.Entities.Rewards;

namespace IdlegharDotnetDomain.Entities
{
    [Serializable()]
    public class Player : PlayerCreds
    {
        public Character? Character { set; get; }
        public int Currency { get; set; } = 0;
        public List<Item> Items { get; set; } = new();
        public List<Reward> UnclaimedRewards { get; private set; } = new();
    }
}