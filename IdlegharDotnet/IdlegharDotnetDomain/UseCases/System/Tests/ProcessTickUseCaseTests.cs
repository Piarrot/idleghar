using IdlegharDotnetDomain.Entities;
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
                Assert.That(oldCharacter!.CurrentQuestState!.Progress, Is.Not.EqualTo(character.CurrentQuestState!.Progress));
            }
        }

        [Test]
        public async Task GivenAQuestingCharacterAndMultipleTicksItShouldCompleteTheQuest()
        {
            var quest = FakeQuestFactory.CreateQuest(Difficulty.EASY);
            var questingCharacter = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);
            var questState = questingCharacter.GetQuestStateOrThrow();

            var useCase = new ProcessTickUseCase(CharactersProvider);
            await useCase.Handle();

            questingCharacter = await CharactersProvider.FindById(questingCharacter.Id);

            while (questingCharacter!.IsQuesting)
            {
                var prevProgress = questingCharacter!.CurrentQuestState!.Progress;
                await useCase.Handle();
                questingCharacter = await CharactersProvider.FindById(questingCharacter.Id);
                if (questingCharacter!.IsQuesting)
                {
                    Assert.That(prevProgress, Is.LessThan(questingCharacter!.CurrentQuestState!.Progress));
                }
            }
            Assert.That(questingCharacter.QuestHistory[0], Is.EqualTo(questState));
            // Assert.That(questingCharacter.QuestHistory[0].Completed, Is.True);

            // var notFailedEncounters = questingCharacter.QuestHistory[0].Previous.FindAll((e) => e.Completed);
            // var rewards = notFailedEncounters.SelectMany((e) => e.Rewards);
            // var player = questingCharacter.Player;
            // Assert.That(player.UnclaimedRewards, Is.EqualTo(rewards));
        }
    }
}