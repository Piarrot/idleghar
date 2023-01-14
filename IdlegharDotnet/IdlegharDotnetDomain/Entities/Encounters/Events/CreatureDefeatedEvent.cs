namespace IdlegharDotnetDomain.Entities.Encounters.Events
{
    [Serializable()]
    public record class CreatureDefeatedEvent(string CreatureName) : EncounterEvent;
}