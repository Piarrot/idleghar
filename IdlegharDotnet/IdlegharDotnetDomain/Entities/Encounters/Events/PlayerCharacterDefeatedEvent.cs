namespace IdlegharDotnetDomain.Entities.Encounters.Events
{
    public record class PlayerCharacterDefeatedEvent(string CharacterName) : EncounterEvent;
}