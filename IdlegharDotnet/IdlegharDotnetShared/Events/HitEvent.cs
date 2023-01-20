namespace IdlegharDotnetShared.Events
{
    [Serializable()]
    public record class HitEvent(string HitterName, string BeingHitName, int Damage) : EncounterEvent();
}