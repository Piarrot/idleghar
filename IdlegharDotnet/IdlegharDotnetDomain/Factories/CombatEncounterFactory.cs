using IdlegharDotnetDomain.Entities.Encounters;
using IdlegharDotnetDomain.Entities.Rewards;
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
            return CreateCombat(RandomnessProvider.GetRandomEncounterDifficultyByQuestDifficulty(difficulty));
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

        public CombatEncounter CreateCombat(Difficulty combatDifficulty)
        {
            var combat = new CombatEncounter(combatDifficulty);


            var desiredHP = this.RandomnessProvider.GetRandomCombatEncounterHPByDifficulty(combatDifficulty);
            combat.Reward.AddXP(Constants.Encounters.CombatXPByEncounterHP(desiredHP));
            this.MaybeAddItemToReward(combatDifficulty, combat.Reward);

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

        private void MaybeAddItemToReward(Difficulty combatDifficulty, Reward reward)
        {
            var quality = this.RandomnessProvider.GetRandomItemQualityEncounterRewardFromDifficulty(combatDifficulty);
            if (!quality.HasValue) return;

            var itemFactory = new EquipmentFactory(this.RandomnessProvider);
            var equipment = itemFactory.CreateEquipment(quality.Value);
            reward.AddItem(equipment);
        }
    }
}