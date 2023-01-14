using IdlegharDotnetDomain.Entities.Encounters.Events;
using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Entities.Encounters.Tests
{
    public class CombatEncounterTests : BaseTests
    {
        [Test]
        public async Task GivenACharacterItShouldAdvanceTheCombatAsync()
        {
            var encounter = new CombatEncounter();

            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Goblin","A Goblin",1,1),
                new EnemyCreature("Goblin","Another Goblin",1,1),
            };

            var quest = FakeQuestFactory.CreateQuest(new() { encounter });
            Character character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);

            var encounterResult = encounter.ProcessEncounter(character);

            Assert.That(encounterResult, Is.EqualTo(EncounterResult.Succeeded));
            Assert.That(character.HP, Is.EqualTo(9));
            Assert.That(((CombatEncounterState)character.CurrentEncounterState!).currentCreatures.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GivenAStrongEnemyItShouldDefeatThePCAsync()
        {
            var encounter = new CombatEncounter();

            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Dragon","A Huge Dragon",2000,20),
            };

            var quest = FakeQuestFactory.CreateQuest(new() { encounter });
            var character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);

            var encounterResult = encounter.ProcessEncounter(character);

            Assert.That(encounterResult, Is.EqualTo(EncounterResult.Failed));
            Assert.That(character.HP, Is.LessThanOrEqualTo(0));
        }

        [Test]
        public async Task GivenAResolvedCombatTheEncounterCreaturesAreNotTouchedAsync()
        {
            var encounter = new CombatEncounter();
            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Goblin","A Goblin",1,1),
                new EnemyCreature("Goblin","Another Goblin",1,1),
            };

            var quest = FakeQuestFactory.CreateQuest(new() { encounter });
            Character character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);

            encounter.ProcessEncounter(character);

            Assert.That(encounter.EnemyCreatures[0].HP, Is.EqualTo(1));
            Assert.That(encounter.EnemyCreatures[1].HP, Is.EqualTo(1));
        }

        [Test]
        public async Task GivenAResolvedCombatItShouldContainTheCombatLogsAsync()
        {
            var encounter = new CombatEncounter();
            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Goblin 1","A Goblin",1,1),
                new EnemyCreature("Goblin 2","Another Goblin",1,1),
            };

            var quest = FakeQuestFactory.CreateQuest(new() { encounter });
            Character character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);

            encounter.ProcessEncounter(character);

            List<EncounterEvent> expectedEventList = new(){
                new HitEvent(character.Name, "Goblin 1", 1),
                new CreatureDefeatedEvent("Goblin 1"),
                new HitEvent("Goblin 2", character.Name, 1),
                new HitEvent(character.Name, "Goblin 2", 1),
                new CreatureDefeatedEvent("Goblin 2"),
                new EnemiesDefeatedEvent(character.Name)
            };

            Assert.That(character.CurrentQuestEvents, Is.EqualTo(expectedEventList));
        }

        [Test]
        public async Task GivenAFailedCombatItShouldContainTheCombatLogsAsync()
        {
            var encounter = new CombatEncounter();

            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Dragon","A Huge Dragon",2000,20),
            };

            var quest = FakeQuestFactory.CreateQuest(new() { encounter });
            var character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);

            encounter.ProcessEncounter(character);

            List<EncounterEvent> expectedEventList = new(){
                new HitEvent(character.Name, "Dragon", 1),
                new HitEvent("Dragon", character.Name, 20),
                new PlayerCharacterDefeatedEvent(character.Name)
            };

            Assert.That(character.CurrentQuestEvents, Is.EqualTo(expectedEventList));
        }

        [Test]
        public async Task ItShouldBePossibleForANewCharacterToWinAnEasyCombatAsync()
        {
            var factory = new Factories.CombatEncounterFactory(RandomnessProvider);

            var encounter = factory.CreateCombat(Constants.Difficulty.EASY);

            var quest = FakeQuestFactory.CreateQuest(new() { encounter });
            Character character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);
            var encounterResult = encounter.ProcessEncounter(character);

            Assert.That(encounterResult, Is.EqualTo(EncounterResult.Succeeded));
            Assert.That(character.HP, Is.EqualTo(4));
        }
    }
}