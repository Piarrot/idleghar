namespace IdlegharDotnetDomain.Entities.Encounters.Events
{
    [Serializable()]
    public record class EnemiesDefeatedEvent(string CharacterName) : EncounterEvent;
}