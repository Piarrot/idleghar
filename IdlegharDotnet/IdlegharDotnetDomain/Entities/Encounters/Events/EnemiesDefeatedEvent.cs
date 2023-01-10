namespace IdlegharDotnetDomain.Entities.Encounters.Events
{
    public record class EnemiesDefeatedEvent(string CharacterName) : EncounterEvent;
}