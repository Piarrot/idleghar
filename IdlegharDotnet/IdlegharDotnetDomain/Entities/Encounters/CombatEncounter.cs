using IdlegharDotnetDomain.Entities.Encounters.Events;

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
            var state = GetStateOrThrow<CombatEncounterState>(character);

            List<EnemyCreature> remainingCreatures = new List<EnemyCreature>();

            bool characterDefeated = false;

            int damageLeftThisRound = character.Damage;

            foreach (var creature in state.currentCreatures)
            {
                var damageToDeal = Math.Min(damageLeftThisRound, creature.HP);
                if (damageToDeal > 0)
                {
                    creature.ReceiveDamage(damageToDeal);
                    character.CurrentQuestEvents.Add(new HitEvent(character.Name, creature.Name, damageToDeal));
                    damageLeftThisRound -= damageToDeal;
                    if (creature.HP <= 0)
                    {
                        character.CurrentQuestEvents.Add(new CreatureDefeatedEvent(creature.Name));
                        continue;
                    }
                }

                remainingCreatures.Add(creature);

                character.ReceiveDamage(creature.Damage);

                character.CurrentQuestEvents.Add(new HitEvent(creature.Name, character.Name, creature.Damage));
                if (character.HP <= 0)
                {
                    characterDefeated = true;
                    character.CurrentQuestEvents.Add(new PlayerCharacterDefeatedEvent(character.Name));
                    break;
                }
            }


            state.currentCreatures = remainingCreatures;
            if (remainingCreatures.Count == 0)
            {
                character.CurrentQuestEvents.Add(new EnemiesDefeatedEvent(character.Name));
                return true;
            }

            if (characterDefeated == true) return true;

            return false;
        }


    }
}