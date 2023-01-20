using IdlegharDotnetShared.Constants;
using IdlegharDotnetShared.Events;

namespace IdlegharDotnetDomain.Entities.Encounters
{
    [Serializable()]
    public class CombatEncounter : Encounter
    {
        public CombatEncounter(Difficulty difficulty = Difficulty.EASY) : base(difficulty)
        {
        }

        public List<EnemyCreature> EnemyCreatures { get; set; } = new List<EnemyCreature>();

        public override CombatEncounterState ProcessEncounter(Character character)
        {
            CombatEncounterState combatState = new CombatEncounterState(this, EnemyCreatures);
            bool characterDefeated = false;
            bool combatDone = false;

            while (!combatDone)
            {
                int damageLeftThisRound = character.Damage;
                List<EnemyCreature> remainingCreatures = new List<EnemyCreature>();

                foreach (var creature in combatState.CurrentCreatures)
                {
                    var damageToDeal = Math.Min(damageLeftThisRound, creature.HP);
                    if (damageToDeal > 0)
                    {
                        creature.ReceiveDamage(damageToDeal);
                        combatState.AddEvent(new HitEvent(character.Name, creature.Name, damageToDeal));
                        damageLeftThisRound -= damageToDeal;
                        if (creature.HP <= 0)
                        {
                            combatState.AddEvent(new CreatureDefeatedEvent(creature.Name));
                            continue;
                        }
                    }

                    remainingCreatures.Add(creature);

                    character.ReceiveDamage(creature.Damage);

                    combatState.AddEvent(new HitEvent(creature.Name, character.Name, creature.Damage));
                    if (character.HP <= 0)
                    {
                        characterDefeated = true;
                        combatDone = true;
                        combatState.AddEvent(new PlayerCharacterDefeatedEvent(character.Name));
                        break;
                    }
                }

                combatState.CurrentCreatures = remainingCreatures;

                if (remainingCreatures.Count == 0)
                {
                    combatState.AddEvent(new EnemiesDefeatedEvent(character.Name));
                    combatDone = true;
                }
            }

            combatState.Result = (characterDefeated == true) ? EncounterResult.Failed : EncounterResult.Succeeded;

            return combatState;
        }


    }
}