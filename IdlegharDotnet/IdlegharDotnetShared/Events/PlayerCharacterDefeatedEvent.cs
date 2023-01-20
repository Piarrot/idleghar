namespace IdlegharDotnetShared.Events
{
    [Serializable()]
    public record class PlayerCharacterDefeatedEvent(string CharacterName) : EncounterEvent;
}