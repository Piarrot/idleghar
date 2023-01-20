namespace IdlegharDotnetShared.Events
{
    [Serializable()]
    public record class CreatureDefeatedEvent(string CreatureName) : EncounterEvent;
}