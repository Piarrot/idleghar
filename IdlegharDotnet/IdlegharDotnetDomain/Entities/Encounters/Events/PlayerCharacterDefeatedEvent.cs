namespace IdlegharDotnetDomain.Entities.Encounters.Events
{
    [Serializable()]
    public record class PlayerCharacterDefeatedEvent(string CharacterName) : EncounterEvent;
}