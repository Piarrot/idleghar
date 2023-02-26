using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Events;
using IdlegharDotnetShared.SharedConstants;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Entities.Encounters.Tests
{
    public class CombatEncounterTests : BaseTests
    {
        [Test]
        public async Task GivenACharacterItShouldAdvanceTheCombat()
        {
            RandomnessProviderMock.Setup((r) => r.GetRandomCombatEncounterHPByDifficulty(Difficulty.NORMAL)).Returns(8);
            var encounter = FakeQuestFactory.CreateCombatEncounter(Difficulty.NORMAL);
            Character character = await FakeCharacterFactory.CreateAndStoreCharacter();
            var encounterState = encounter.ProcessEncounter(character);

            Assert.That(encounterState.Result, Is.EqualTo(EncounterResult.Succeeded));
            Assert.That(character.HP, Is.Not.EqualTo(10));
            Assert.That(encounterState.Completed, Is.True);
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
            RandomnessProviderMock.Setup((r) => r.GetRandomCombatEncounterHPByDifficulty(Difficulty.EASY)).Returns(2);
            var encounter = FakeQuestFactory.CreateCombatEncounter(Difficulty.EASY);

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
                new HitEvent(character.Name, "Dragon", character.Damage),
                new HitEvent("Dragon", character.Name, 20),
                new PlayerCharacterDefeatedEvent(character.Name)
            };

            Assert.That(state.EncounterEvents, Is.EqualTo(expectedEventList));
        }

        [Test]
        public async Task ItShouldBePossibleForANewCharacterToWinAnEasyCombat()
        {
            var encounter = FakeQuestFactory.CreateCombatEncounter(Difficulty.EASY);

            var quest = FakeQuestFactory.CreateQuest(new List<Encounter>() { encounter });
            Character character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);
            var encounterState = encounter.ProcessEncounter(character);

            Assert.That(encounterState.Result, Is.EqualTo(EncounterResult.Succeeded));
            Assert.That(character.HP, Is.EqualTo(10));
        }
    }
}