namespace IdlegharDotnetDomain.Entities.Encounters
{
    public class CombatEncounterState : EncounterState
    {
        public List<EnemyCreature> currentCreatures;

        public CombatEncounterState(Encounter encounter, List<EnemyCreature> startingCreatures) : base(encounter)
        {
            currentCreatures = startingCreatures.ConvertAll((creatureTemplate) =>
            {
                return creatureTemplate.Clone();
            });
        }
    }
}