namespace IdlegharDotnetDomain.Entities.Encounters
{
    public class CombatEncounter : Encounter
    {
        private List<EnemyCreature> EnemyCreatures { get; set; } = new List<EnemyCreature>();

        public override EncounterState GetNewState()
        {
            return new CombatEncounterState(this, EnemyCreatures);
        }

        public override bool ProcessTick(Character character)
        {
            CombatEncounterState state = GetStateOrThrow(character);

            List<EnemyCreature> remainingCreatures = new List<EnemyCreature>();

            bool characterDefeated = false;

            foreach (var creature in state.RemainingCreatures)
            {
                character.ReceiveDamage(creature.Damage);
                creature.ReceiveDamage(character.Damage);
                if (creature.HP > 0)
                {
                    remainingCreatures.Add(creature);
                }

                if (character.HP <= 0)
                {
                    characterDefeated = true;
                    break;
                }
            }

            if (characterDefeated == true || remainingCreatures.Count == 0) return true;

            return false;
        }

        public void SetEnemies(List<EnemyCreature> enemyCreatures)
        {
            this.EnemyCreatures = enemyCreatures;
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