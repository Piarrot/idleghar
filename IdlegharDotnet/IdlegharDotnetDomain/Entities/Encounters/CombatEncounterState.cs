namespace IdlegharDotnetDomain.Entities.Encounters
{
    [Serializable()]
    public class CombatEncounterState : EncounterState
    {
        public List<EnemyCreature> CurrentCreatures { get; set; }

        public CombatEncounterState(Encounter encounter, List<EnemyCreature> startingCreatures) : base(encounter)
        {
            CurrentCreatures = startingCreatures.ConvertAll((creatureTemplate) =>
            {
                return creatureTemplate.Clone();
            });
        }

    }
}