namespace IdlegharDotnetDomain.Entities.Encounters
{
    public class CombatEncounterState : EncounterState
    {
        public List<EnemyCreature> RemainingCreatures;

        public CombatEncounterState(Encounter encounter, List<EnemyCreature> startingCreatures) : base(encounter)
        {
            RemainingCreatures = startingCreatures;
        }
    }
}