using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetDomain.Entities.Rewards;

namespace IdlegharDotnetDomain.Entities
{
    [Serializable()]
    public class Player : Entity
    {
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;
        public bool EmailValidated { get; set; } = false;
        public string? EmailValidationCode { get; set; }
        public Character? Character { get; set; }

        public List<Item> Items { get; set; } = new();
        public int Currency { get; set; } = 0;

        public bool HasCharacter => Character != null;

        public List<Reward> UnclaimedRewards { get; private set; } = new();

        public Character GetCharacterOrThrow()
        {
            if (Character == null)
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_CREATED);
            return Character;
        }
    }
}