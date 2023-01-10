namespace IdlegharDotnetDomain.Entities.Encounters.Events
{
    public record class CreatureDefeatedEvent(string CreatureName) : EncounterEvent;
}