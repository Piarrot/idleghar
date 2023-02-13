using IdlegharDotnetDomain.Entities.Encounters;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Factories
{
    public class CombatEncounterFactory
    {
        IRandomnessProvider RandomnessProvider;

        public CombatEncounterFactory(IRandomnessProvider randomnessProvider)
        {
            RandomnessProvider = randomnessProvider;
        }

        public CombatEncounter CreateCombatFromQuestDifficulty(Difficulty difficulty)
        {
            return CreateCombat(Constants.Encounters.EncounterDifficultyByQuestDifficulty[difficulty].ResolveOne(RandomnessProvider));
        }

        public List<CombatEncounter> CreateCombatsFromQuestDifficulty(Difficulty difficulty, int count)
        {
            var result = new List<CombatEncounter>();
            for (int i = 0; i < count; i++)
            {
                result.Add(CreateCombatFromQuestDifficulty(difficulty));
            }
            return result;
        }

        internal CombatEncounter CreateCombat(Difficulty combatDifficulty)
        {
            var combat = new CombatEncounter(combatDifficulty);

            var desiredHP = Constants.Encounters.EnemyHPByDifficulty[combatDifficulty];
            var encounterTotalHP = 0;
            var index = 1;
            while (encounterTotalHP != desiredHP)
            {
                var creature = new EnemyCreature($"Goblin {index}", "A Goblin", 1, 1);
                combat.EnemyCreatures.Add(creature);
                encounterTotalHP += creature.HP;
                index++;
            }

            return combat;
        }
    }
}