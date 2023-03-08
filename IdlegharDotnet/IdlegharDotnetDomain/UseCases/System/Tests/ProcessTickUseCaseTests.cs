using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Entities.Rewards;
using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.SharedConstants;
using Moq;
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

            var useCase = new ProcessTickUseCase(StorageProvider);
            await useCase.Handle();

            List<Character> updatedNonQuestingCharacters = await StorageProvider.FindAllCharactersNotQuesting();
            Assert.That(updatedNonQuestingCharacters.Count, Is.GreaterThanOrEqualTo(nonQuestingCharacters.Count));

            List<Character> updatedQuestingCharacters = await StorageProvider.FindAllCharactersQuesting();
            Assert.That(updatedQuestingCharacters.Count, Is.LessThanOrEqualTo(questingCharacters.Count));

            foreach (var character in updatedQuestingCharacters)
            {
                var oldCharacter = questingCharacters.Find(c => c.Id == character.Id);
                Assert.That(oldCharacter!.CurrentQuestState!.Progress, Is.Not.EqualTo(character.CurrentQuestState!.Progress));
            }
        }

        [Test]
        public async Task GivenAQuestingCharacterAndMultipleTicksItShouldCompleteTheQuestAndGetTheRewards()
        {
            RandomnessProviderMock.Setup(MockRandomIntLambda).Returns(1);
            var quest = FakeQuestFactory.CreateQuest(Difficulty.EASY);
            var questingCharacter = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);
            var questState = questingCharacter.GetQuestStateOrThrow();

            var useCase = new ProcessTickUseCase(StorageProvider);
            await useCase.Handle();

            questingCharacter = await StorageProvider.FindCharacterById(questingCharacter.Id);

            while (questingCharacter!.IsQuesting)
            {
                var prevProgress = questingCharacter!.CurrentQuestState!.Progress;
                await useCase.Handle();
                questingCharacter = await StorageProvider.FindCharacterById(questingCharacter.Id);
                if (questingCharacter!.IsQuesting)
                {
                    Assert.That(prevProgress, Is.LessThan(questingCharacter!.CurrentQuestState!.Progress));
                }
            }
            Assert.That(questingCharacter.QuestHistory[0], Is.EqualTo(questState));
            Assert.That(questingCharacter.QuestHistory[0].Completed, Is.True);

            var reward = questingCharacter.QuestHistory[0].Quest.Reward;
            Assert.That(questingCharacter.Owner.UnclaimedRewards, Does.Contain(reward));

            var encounterRewards = questingCharacter.QuestHistory[0].Previous.Select((eS) => eS.Encounter.Reward);
            foreach (var encounterReward in encounterRewards)
            {
                Assert.That(questingCharacter.Owner.UnclaimedRewards, Does.Contain(encounterReward));
            }
        }
    }
}