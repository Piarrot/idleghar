using IdlegharDotnetShared.Characters;
using IdlegharDotnetShared.Items;
using IdlegharDotnetShared.Rewards;

namespace IdlegharDotnetShared
{
    public class PlayerViewModel : EntityViewModel
    {
        public string Email { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;
        public int Currency { get; set; } = 0;
        public List<ItemViewModel> Items { get; set; } = new();
        public List<RewardViewModel> UnclaimedRewards { get; set; } = new();
        public CharacterViewModel? Character;
    }
}