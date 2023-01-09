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

            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Goblin","A Goblin",1,1),
                new EnemyCreature("Goblin","Another Goblin",1,1),
            };

            var quest = FakeQuestFactory.CreateQuest(new List<Encounter>() { encounter });
            Character character = FakeCharacterFactory.CreateCharacterWithQuest(quest);

            var encounterEnded = encounter.ProcessTick(character);

            Assert.IsFalse(encounterEnded);
            Assert.AreEqual(9, character.HP);

            encounterEnded = encounter.ProcessTick(character);
            Assert.IsTrue(encounterEnded);
            Assert.AreEqual(9, character.HP);
            Assert.AreEqual(0, ((CombatEncounterState)character.CurrentEncounterState!).currentCreatures.Count);
        }

        [Test]
        public void GivenAStrongEnemyItShouldDefeatThePC()
        {
            var encounter = new CombatEncounter();

            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Dragon","A Huge Dragon",2000,20),
            };

            var quest = FakeQuestFactory.CreateQuest(new List<Encounter>() { encounter });
            var character = FakeCharacterFactory.CreateCharacterWithQuest(quest);

            var encounterEnded = encounter.ProcessTick(character);

            Assert.IsTrue(encounterEnded);
            Assert.LessOrEqual(character.HP, 0);
        }

        [Test]
        public void GivenAResolvedCombatTheEncounterCreaturesAreNotTouched()
        {
            var encounter = new CombatEncounter();
            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Goblin","A Goblin",1,1),
                new EnemyCreature("Goblin","Another Goblin",1,1),
            };

            var quest = FakeQuestFactory.CreateQuest(new List<Encounter>() { encounter });
            Character character = FakeCharacterFactory.CreateCharacterWithQuest(quest);

            encounter.ProcessTick(character);
            encounter.ProcessTick(character);

            Assert.AreEqual(1, encounter.EnemyCreatures[0].HP);
            Assert.AreEqual(1, encounter.EnemyCreatures[1].HP);
        }
    }
}