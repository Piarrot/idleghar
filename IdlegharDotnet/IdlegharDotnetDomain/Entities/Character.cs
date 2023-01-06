namespace IdlegharDotnetDomain.Entities
{
    public class Character : Entity
    {
        public string Name { get; set; }
        public Quest? CurrentQuest { get; set; }
        public Encounter? CurrentEncounter { get; set; }
        public bool IsQuesting => CurrentQuest != null && CurrentEncounter != null;

        public Encounter GetEncounterOrThrow()
        {
            if (CurrentQuest == null || CurrentEncounter == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_QUESTING);
            }
            return CurrentEncounter;
        }

        public void ThrowIfNotQuesting()
        {
            if (CurrentQuest != null || CurrentEncounter != null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_ALREADY_QUESTING);
            }
        }
    }
}