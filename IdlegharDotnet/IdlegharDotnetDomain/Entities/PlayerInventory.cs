using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetDomain.Entities.Rewards;

namespace IdlegharDotnetDomain.Entities
{
    public class PlayerInventory
    {
        public List<Item> Items { get; set; } = new();
        public int Currency { get; set; } = 0;

        public List<Reward> UnclaimedRewards { get; private set; } = new();

    }
}