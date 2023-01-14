namespace IdlegharDotnetDomain.Entities.Encounters.Events
{
    [Serializable()]
    public record class HitEvent(string HitterName, string BeingHitName, int Damage) : EncounterEvent();
}