using IdlegharDotnetDomain.Constants;
using IdlegharDotnetDomain.Entities.Encounters.Events;

namespace IdlegharDotnetDomain.Entities.Encounters
{
    [Serializable()]
    public class CombatEncounter : Encounter
    {
        public CombatEncounter(Difficulty difficulty = Difficulty.EASY) : base(difficulty)
        {
        }

        public List<EnemyCreature> EnemyCreatures { get; set; } = new List<EnemyCreature>();

        public override EncounterState GetNewState()
        {
            return new CombatEncounterState(this, EnemyCreatures);
        }

        public override EncounterResult ProcessEncounter(Character character)
        {
            QuestState questState = character.GetQuestStateOrThrow();
            CombatEncounterState state = questState.GetCurrentEncounterStateOrThrow<CombatEncounterState>();
            bool characterDefeated = false;
            bool combatDone = false;

            while (!combatDone)
            {
                int damageLeftThisRound = character.Damage;
                List<EnemyCreature> remainingCreatures = new List<EnemyCreature>();

                foreach (var creature in state.CurrentCreatures)
                {
                    var damageToDeal = Math.Min(damageLeftThisRound, creature.HP);
                    if (damageToDeal > 0)
                    {
                        creature.ReceiveDamage(damageToDeal);
                        questState.AddEvent(new HitEvent(character.Name, creature.Name, damageToDeal));
                        damageLeftThisRound -= damageToDeal;
                        if (creature.HP <= 0)
                        {
                            questState.AddEvent(new CreatureDefeatedEvent(creature.Name));
                            continue;
                        }
                    }

                    remainingCreatures.Add(creature);

                    character.ReceiveDamage(creature.Damage);

                    questState.AddEvent(new HitEvent(creature.Name, character.Name, creature.Damage));
                    if (character.HP <= 0)
                    {
                        characterDefeated = true;
                        combatDone = true;
                        questState.AddEvent(new PlayerCharacterDefeatedEvent(character.Name));
                        break;
                    }
                }

                state.CurrentCreatures = remainingCreatures;

                if (remainingCreatures.Count == 0)
                {
                    questState.AddEvent(new EnemiesDefeatedEvent(character.Name));
                    combatDone = true;
                }
            }

            state.Result = (characterDefeated == true) ? EncounterResult.Failed : EncounterResult.Succeeded;

            return state.Result;
        }


    }
}