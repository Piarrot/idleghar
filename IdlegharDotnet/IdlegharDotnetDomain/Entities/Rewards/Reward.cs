namespace IdlegharDotnetDomain.Entities.Rewards
{
    [Serializable()]
    public abstract class Reward : Entity
    {
        public bool Claimed { get; private set; } = false;
        public abstract void Claim(Character character);
    }
}