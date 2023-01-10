namespace IdlegharDotnetDomain.Entities.Encounters.Events
{
    public record class HitEvent(string HitterName, string BeingHitName, int Damage) : EncounterEvent();
}