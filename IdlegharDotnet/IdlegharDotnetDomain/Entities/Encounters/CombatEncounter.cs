namespace IdlegharDotnetDomain.Entities.Encounters
{
    public class CombatEncounter : Encounter
    {
        public List<EnemyCreature> EnemyCreatures { get; set; } = new List<EnemyCreature>();

        public override EncounterState GetNewState()
        {
            return new CombatEncounterState(this, EnemyCreatures);
        }

        public override bool ProcessTick(Character character)
        {
            CombatEncounterState state = GetStateOrThrow(character);

            List<EnemyCreature> remainingCreatures = new List<EnemyCreature>();

            bool characterDefeated = false;

            int damageLeftThisRound = character.Damage;

            foreach (var creature in state.currentCreatures)
            {
                var damageDealt = Math.Min(damageLeftThisRound, creature.HP);
                creature.ReceiveDamage(damageDealt);
                damageLeftThisRound -= damageDealt;
                if (creature.HP <= 0)
                {
                    continue;
                }

                remainingCreatures.Add(creature);

                character.ReceiveDamage(creature.Damage);
                if (character.HP <= 0)
                {
                    characterDefeated = true;
                    break;
                }
            }

            state.currentCreatures = remainingCreatures;
            if (characterDefeated == true || remainingCreatures.Count == 0) return true;

            return false;
        }

        public CombatEncounterState GetStateOrThrow(Character character)
        {
            CombatEncounterState? state = character.CurrentEncounterState as CombatEncounterState;
            if (state == null)
                throw new InvalidOperationException(Constants.ErrorMessages.INVALID_ENCOUNTER_STATE);
            return state;
        }
    }
}