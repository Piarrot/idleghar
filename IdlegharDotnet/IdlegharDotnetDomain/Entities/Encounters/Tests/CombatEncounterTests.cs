using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Constants;
using IdlegharDotnetShared.Events;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Entities.Encounters.Tests
{
    public class CombatEncounterTests : BaseTests
    {
        [Test]
        public async Task GivenACharacterItShouldAdvanceTheCombat()
        {
            var encounter = new CombatEncounter();

            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Goblin","A Goblin",1,1),
                new EnemyCreature("Goblin","Another Goblin",1,1),
            };

            var quest = FakeQuestFactory.CreateQuest(new List<Encounter>() { encounter });
            Character character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);

            var encounterState = encounter.ProcessEncounter(character);

            Assert.That(encounterState.Result, Is.EqualTo(EncounterResult.Succeeded));
            Assert.That(character.HP, Is.EqualTo(9));
            Assert.That(encounterState.CurrentCreatures.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GivenAStrongEnemyItShouldDefeatThePC()
        {
            var encounter = new CombatEncounter();

            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Dragon","A Huge Dragon",2000,20),
            };

            var quest = FakeQuestFactory.CreateQuest(new List<Encounter>() { encounter });
            var character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);

            var encounterState = encounter.ProcessEncounter(character);

            Assert.That(encounterState.Result, Is.EqualTo(EncounterResult.Failed));
            Assert.That(character.HP, Is.LessThanOrEqualTo(0));
        }

        [Test]
        public async Task GivenAResolvedCombatTheEncounterCreaturesAreNotTouched()
        {
            var encounter = new CombatEncounter();
            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Goblin","A Goblin",1,1),
                new EnemyCreature("Goblin","Another Goblin",1,1),
            };

            var quest = FakeQuestFactory.CreateQuest(new List<Encounter>() { encounter });
            Character character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);

            encounter.ProcessEncounter(character);

            Assert.That(encounter.EnemyCreatures[0].HP, Is.EqualTo(1));
            Assert.That(encounter.EnemyCreatures[1].HP, Is.EqualTo(1));
        }

        [Test]
        public async Task GivenAResolvedCombatItShouldContainTheCombatLogs()
        {
            var encounter = new CombatEncounter();
            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Goblin 1","A Goblin",1,1),
                new EnemyCreature("Goblin 2","Another Goblin",1,1),
            };

            var quest = FakeQuestFactory.CreateQuest(new List<Encounter>() { encounter });
            Character character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);

            var state = encounter.ProcessEncounter(character);

            List<EncounterEvent> expectedEventList = new(){
                new HitEvent(character.Name, "Goblin 1", 1),
                new CreatureDefeatedEvent("Goblin 1"),
                new HitEvent("Goblin 2", character.Name, 1),
                new HitEvent(character.Name, "Goblin 2", 1),
                new CreatureDefeatedEvent("Goblin 2"),
                new EnemiesDefeatedEvent(character.Name)
            };

            Assert.That(state.EncounterEvents, Is.EqualTo(expectedEventList));
        }

        [Test]
        public async Task GivenAFailedCombatItShouldContainTheCombatLogs()
        {
            var encounter = new CombatEncounter();

            encounter.EnemyCreatures = new List<EnemyCreature>{
                new EnemyCreature("Dragon","A Huge Dragon",2000,20),
            };

            var quest = FakeQuestFactory.CreateQuest(new List<Encounter>() { encounter });
            var character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);

            var state = encounter.ProcessEncounter(character);

            List<EncounterEvent> expectedEventList = new(){
                new HitEvent(character.Name, "Dragon", 1),
                new HitEvent("Dragon", character.Name, 20),
                new PlayerCharacterDefeatedEvent(character.Name)
            };

            Assert.That(state.EncounterEvents, Is.EqualTo(expectedEventList));
        }

        [Test]
        public async Task ItShouldBePossibleForANewCharacterToWinAnEasyCombat()
        {
            var factory = new Factories.CombatEncounterFactory(RandomnessProvider);

            var encounter = factory.CreateCombat(Difficulty.EASY);

            var quest = FakeQuestFactory.CreateQuest(new List<Encounter>() { encounter });
            Character character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);
            var encounterState = encounter.ProcessEncounter(character);

            Assert.That(encounterState.Result, Is.EqualTo(EncounterResult.Succeeded));
            Assert.That(character.HP, Is.EqualTo(4));
        }
    }
}