using IdlegharDotnetDomain.Constants;
using IdlegharDotnetDomain.Entities.Random;
using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Constants;
using Moq;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Factories.Tests
{
    public class CombatEncounterFactoryTests : BaseTests
    {
        [Test]
        public void ItShouldGenerateEnemiesBasedOnCombatDifficulty()
        {
            var factory = new CombatEncounterFactory(RandomnessProviderMock.Object);

            foreach (Difficulty combatDifficulty in Enum.GetValues(typeof(Difficulty)))
            {
                var encounter = factory.CreateCombat(combatDifficulty);
                var totalHP = encounter.EnemyCreatures.Sum(c => c.HP);
                Assert.That(Constants.Encounters.EnemyHPByDifficulty[combatDifficulty].Matches(totalHP), Is.True);
            }
        }

        [Test]
        public void ItShouldGenerateXpRewardsBasedOnDifficulty()
        {
            var factory = new CombatEncounterFactory(RandomnessProviderMock.Object);

            foreach (Difficulty combatDifficulty in Enum.GetValues(typeof(Difficulty)))
            {
                var encounter = factory.CreateCombat(combatDifficulty);
                var xp = encounter.Reward.XP;
                Assert.That(xp, Is.EqualTo(Encounters.CombatXPByEncounterHP(encounter.EnemyCreatures.Sum(c => c.HP))));
            }
        }

        [Test]
        public void ItShouldGenerateItemRewardsBasedOnDifficulty()
        {
            foreach (ItemQuality itemQuality in Enum.GetValues(typeof(ItemQuality)))
            {
                RandomnessProviderMock.Setup((r) => r.GetRandomItemQualityEncounterRewardFromDifficulty(Difficulty.EASY)).Returns(Optional<ItemQuality>.Some(itemQuality));
                var factory = new CombatEncounterFactory(RandomnessProviderMock.Object);

                var encounter = factory.CreateCombat(Difficulty.EASY);
                var items = encounter.Reward.Items;
                Assert.That(items, Is.Not.Empty);
            }
        }

        [Test]
        public void ItShouldSometimesNotGenerateItemReward()
        {
            RandomnessProviderMock.Setup((r) => r.GetRandomItemQualityEncounterRewardFromDifficulty(Difficulty.EASY)).Returns(Optional<ItemQuality>.None());
            var factory = new CombatEncounterFactory(RandomnessProviderMock.Object);

            var encounter = factory.CreateCombat(Difficulty.EASY);
            var items = encounter.Reward.Items;
            Assert.That(items, Is.Empty);
        }
    }
}