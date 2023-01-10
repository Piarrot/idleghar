using IdlegharDotnetDomain.Entities.Encounters.Events;
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

        [Test]
        public void GivenAResolvedCombatItShouldContainTheCombatLogs()
        {
            var encounter = new CombatEncounter();
            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Goblin 1","A Goblin",1,1),
                new EnemyCreature("Goblin 2","Another Goblin",1,1),
            };

            var quest = FakeQuestFactory.CreateQuest(new List<Encounter>() { encounter });
            Character character = FakeCharacterFactory.CreateCharacterWithQuest(quest);

            encounter.ProcessTick(character);
            encounter.ProcessTick(character);

            List<EncounterEvent> expectedEventList = new List<EncounterEvent>(){
                new HitEvent(character.Name, "Goblin 1", 1),
                new CreatureDefeatedEvent("Goblin 1"),
                new HitEvent("Goblin 2", character.Name, 1),
                new HitEvent(character.Name, "Goblin 2", 1),
                new CreatureDefeatedEvent("Goblin 2"),
                new EnemiesDefeatedEvent(character.Name)
            };

            Assert.AreEqual(expectedEventList, character.CurrentQuestEvents);
        }

        [Test]
        public void GivenAFailedCombatItShouldContainTheCombatLogs()
        {
            var encounter = new CombatEncounter();

            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Dragon","A Huge Dragon",2000,20),
            };

            var quest = FakeQuestFactory.CreateQuest(new List<Encounter>() { encounter });
            var character = FakeCharacterFactory.CreateCharacterWithQuest(quest);

            var encounterEnded = encounter.ProcessTick(character);

            List<EncounterEvent> expectedEventList = new List<EncounterEvent>(){
                new HitEvent(character.Name, "Dragon", 1),
                new HitEvent("Dragon", character.Name, 20),
                new PlayerCharacterDefeatedEvent(character.Name)
            };

            Assert.AreEqual(expectedEventList, character.CurrentQuestEvents);
        }
    }
}