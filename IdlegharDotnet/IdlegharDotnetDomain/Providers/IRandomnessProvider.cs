using IdlegharDotnetDomain.Entities.Random;
using IdlegharDotnetShared.Constants;
using static IdlegharDotnetDomain.Constants.Characters;

namespace IdlegharDotnetDomain.Providers
{
    public interface IRandomnessProvider
    {
        double GetRandomDouble(double min, double max);

        int GetRandomInt(int min, int max);

        int GetRandomQuestCountByDifficulty(Difficulty questDifficulty);

        Optional<ItemQuality> GetRandomItemQualityQuestRewardFromDifficulty(Difficulty questDifficulty);

        int GetRandomItemAbilityIncreaseByItemQuality(ItemQuality quality);

        EquipmentType GetRandomEquipmentType();

        Difficulty GetRandomEncounterDifficultyByQuestDifficulty(Difficulty questDifficulty);

        Stat GetRandomStat();

        Difficulty GetRandomDifficulty();
        int GetRandomCombatEncounterHPByDifficulty(Difficulty combatDifficulty);

        Optional<ItemQuality> GetRandomItemQualityEncounterRewardFromDifficulty(Difficulty encounterDifficulty);
    }
}