using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Entities.Encounters.Tests
{
    public class CombatEncounterTests : BaseTests
    {
        [Test]
        public void GivenACharacterItShouldAdvanceTheCombat()
        {
            var encounter = new CombatEncounter();

            encounter.SetEnemies(new List<EnemyCreature>{
                new EnemyCreature("Goblin","A Goblin",2,1),
                new EnemyCreature("Goblin","A Goblin",2,1),
                new EnemyCreature("Goblin","A Goblin",2,1),
                new EnemyCreature("Goblin","A Goblin",2,1),
            });

            var quest = FakeQuestFactory.CreateQuest(new List<Encounter>() { encounter });
            var character = FakeCharacterFactory.CreateCharacterWithQuest(quest);

            var encounterEnded = encounter.ProcessTick(character);

            Assert.IsFalse(encounterEnded);
            Assert.AreEqual(character.HP, 6);
        }
    }
}