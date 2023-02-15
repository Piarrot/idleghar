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
                Assert.That(totalHP, Is.EqualTo(Constants.Encounters.EnemyHPByDifficulty[combatDifficulty]));
            }
        }
    }
}