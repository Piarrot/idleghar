using IdlegharDotnetDomain.Entities.Encounters;
using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Constants;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Factories.Tests
{
    public class CombatEncounterFactoryTests : BaseTests
    {
        readonly int GENERATION_COUNT = 1000;

        [Test]
        public void ItShouldGenerateCombatsOfDifficultyBasedOnQuestDifficulty()
        {
            var factory = new CombatEncounterFactory(RandomnessProvider);

            foreach (Difficulty questDifficulty in Enum.GetValues(typeof(Difficulty)))
            {
                var encounters = factory.CreateCombatsFromQuestDifficulty(questDifficulty, GENERATION_COUNT);
                AssertEncountersChanceFor(encounters, questDifficulty);
            }
        }

        [Test]
        public void ItShouldGenerateEnemiesBasedOnCombatDifficulty()
        {
            var factory = new CombatEncounterFactory(RandomnessProvider);

            foreach (Difficulty combatDifficulty in Enum.GetValues(typeof(Difficulty)))
            {
                var encounter = factory.CreateCombat(combatDifficulty);
                var totalHP = encounter.EnemyCreatures.Sum(c => c.HP);
                Assert.That(totalHP, Is.EqualTo(Constants.Encounters.EnemyHPByDifficulty[combatDifficulty]));
            }
        }


        private void AssertEncountersChanceFor(List<CombatEncounter> encounters, Difficulty questDifficulty)
        {
            var encounterRandomDiff = Constants.Encounters.EncounterDifficultyByQuestDifficulty;
            foreach (Difficulty encounterDifficulty in Enum.GetValues(typeof(Difficulty)))
            {
                var easyEncounters = encounters.Sum(e => e.Difficulty == encounterDifficulty ? 1 : 0);
                double aproxChance = (double)easyEncounters / GENERATION_COUNT;
                Assert.That(encounterRandomDiff[questDifficulty].Matches(encounterDifficulty, aproxChance), Is.True, $"QuestDifficulty: {questDifficulty} EncounterDifficulty: {encounterDifficulty}, aproxChance: {aproxChance}");
            }
        }
    }
}