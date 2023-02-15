using System.Collections.ObjectModel;
using IdlegharDotnetDomain.Entities.Items;

namespace IdlegharDotnetDomain.Entities.Rewards
{
    [Serializable()]
    public class Reward : Entity
    {
        public bool Claimed { get; private set; } = false;
        public int XP { get; private set; }
        private List<Item> items = new();

        public ReadOnlyCollection<Item> Items
        {
            get
            {
                return new ReadOnlyCollection<Item>(this.items);
            }
        }

        public void Claim(Player player)
        {
            player.UnclaimedRewards.Remove(this);
            this.ApplyXP(player);
            this.ApplyItems(player);
        }

        private void ApplyXP(Player player)
        {
            player.Character!.AddXP(this.XP);
        }

        private void ApplyItems(Player player)
        {
            player.Items.AddRange(this.items);
        }

        public void AddXP(int xpToAdd)
        {
            this.XP += xpToAdd;
        }

        internal void AddItem(Item equipment)
        {
            this.items.Add(equipment);
        }
    }
}