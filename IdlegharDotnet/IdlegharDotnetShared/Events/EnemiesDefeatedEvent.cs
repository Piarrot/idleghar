namespace IdlegharDotnetShared.Events
{
    [Serializable()]
    public record class EnemiesDefeatedEvent(string CharacterName) : EncounterEvent;
}