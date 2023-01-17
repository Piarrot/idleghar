using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Entities.Encounters;
using IdlegharDotnetDomain.Entities.Quests;
using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Constants;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.System.Tests
{
    public class ProcessTickUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenAFewQuestingCharactersItShouldUpdateTheirEncounters()
        {
            var questingCharacters = await FakeCharacterFactory.CreateAndStoreMultipleCharactersWithQuests(10);
            var nonQuestingCharacters = await FakeCharacterFactory.CreateAndStoreMultipleCharacters(10);

            var useCase = new ProcessTickUseCase(CharactersProvider);
            await useCase.Handle();

            List<Character> updatedNonQuestingCharacters = await CharactersProvider.FindAllNotQuesting();
            Assert.That(updatedNonQuestingCharacters.Count, Is.GreaterThanOrEqualTo(nonQuestingCharacters.Count));

            List<Character> updatedQuestingCharacters = await CharactersProvider.FindAllQuesting();
            Assert.That(updatedQuestingCharacters.Count, Is.LessThanOrEqualTo(questingCharacters.Count));

            foreach (var character in updatedQuestingCharacters)
            {
                var oldCharacter = questingCharacters.Find(c => c.Id == character.Id);
                Assert.That(oldCharacter!.GetEncounterOrThrow().Id, Is.Not.EqualTo(character.GetEncounterOrThrow().Id));
            }
        }

        [Test]
        public async Task GivenAQuestingCharacterAndMultipleTicksItShouldCompleteTheQuest()
        {
            var questingCharacter = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest();
            var useCase = new ProcessTickUseCase(CharactersProvider);
            await useCase.Handle();

            var questState = questingCharacter.GetQuestStateOrThrow();

            questingCharacter = await CharactersProvider.FindById(questingCharacter.Id);
            while (questingCharacter!.IsQuesting)
            {
                await useCase.Handle();
                questingCharacter = await CharactersProvider.FindById(questingCharacter.Id);
            }

            Assert.That(questingCharacter.QuestHistory.Contains(questState), Is.True);
        }

        [Test]
        public async Task GivenAQuestingCharacterThatFailsAnEncounterTheQuestShouldRemainIncomplete()
        {
            var quest = FakeQuestFactory.CreateQuest(Difficulty.LEGENDARY);
            var questingCharacter = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest();
            var useCase = new ProcessTickUseCase(CharactersProvider);
            await useCase.Handle();

            var questState = questingCharacter.GetQuestStateOrThrow();

            questingCharacter = await CharactersProvider.FindById(questingCharacter.Id);
            while (questingCharacter!.IsQuesting)
            {
                Assert.That(questingCharacter.HP, Is.GreaterThan(0)); //Shouldn't be 0 if they're still questing
                await useCase.Handle();
                questingCharacter = await CharactersProvider.FindById(questingCharacter.Id);
            }

            var failedQuestState = questingCharacter.QuestHistory.First();
            Assert.That(failedQuestState.Completed, Is.False);
            Assert.That(failedQuestState.Status, Is.EqualTo(QuestStatus.Failed));
        }
    }
}