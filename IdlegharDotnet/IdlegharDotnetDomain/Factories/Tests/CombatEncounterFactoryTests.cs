using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Constants;
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
        public void ItShouldGenerateXpAndItemRewardsBasedOnDifficulty()
        {
            RandomnessProviderMock.Setup(MockRandomIntLambda).Returns()
            var factory = new CombatEncounterFactory(RandomnessProviderMock.Object);

            foreach (Difficulty combatDifficulty in Enum.GetValues(typeof(Difficulty)))
            {
                var encounter = factory.CreateCombat(combatDifficulty);
                var xp = encounter.Reward.XP;
                Assert.That(xp, Is.GreaterThan(0));

            }
        }
    }
}