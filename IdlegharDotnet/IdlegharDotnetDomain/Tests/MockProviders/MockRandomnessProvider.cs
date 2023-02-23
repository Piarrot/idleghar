using IdlegharDotnetDomain.Constants;
using IdlegharDotnetDomain.Entities.Random;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Tests.MockProviders
{
    public class MockRandomnessProvider : IRandomnessProvider
    {
        private Random RNG;

        public MockRandomnessProvider()
        {
            this.RNG = new Random("pizza".GetHashCode());
        }

        public virtual int GetRandomInt(int min, int max)
        {
            return RNG.Next(min, max + 1);
        }

        public virtual double GetRandomDouble(double min, double max)
        {
            return min + (RNG.NextDouble() * max);
        }

        public virtual int GetRandomQuestCountByDifficulty(Difficulty questDifficulty)
        {
            return Quests.QuestCountByDifficulty[questDifficulty].ResolveOne(this);
        }

        public virtual int GetRandomItemAbilityIncreaseByItemQuality(ItemQuality quality)
        {
            return EquipmentItems.RandomAbilityIncrease[quality].ResolveOne(this);
        }

        public virtual EquipmentType GetRandomEquipmentType()
        {
            return EquipmentItems.RandomEquipmentType.ResolveOne(this);
        }

        public virtual Difficulty GetRandomEncounterDifficultyByQuestDifficulty(Difficulty questDifficulty)
        {
            return Encounters.EncounterDifficultyByQuestDifficulty[questDifficulty].ResolveOne(this);
        }

        public virtual Characters.Stat GetRandomStat()
        {
            return Characters.RandomStat.ResolveOne(this);
        }

        public virtual Optional<ItemQuality> GetRandomItemQualityQuestRewardFromDifficulty(Difficulty questDifficulty)
        {
            return Quests.QuestItemRewardChances[questDifficulty].ResolveOne(this);
        }

        public virtual Difficulty GetRandomDifficulty()
        {
            return Quests.RandomDifficulty.ResolveOne(this);
        }

        public virtual int GetRandomCombatEncounterHPByDifficulty(Difficulty combatDifficulty)
        {
            return Constants.Encounters.EnemyHPByDifficulty[combatDifficulty].ResolveOne(this);
        }
    }
}