using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.System.Tests
{
    public class ProcessTickUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenAFewQuestingCharactersItShouldUpdateTheirEncountersAsync()
        {
            var questingCharacters = await FakeCharacterFactory.CreateAndStoreMultipleCharactersWithQuests(10);
            var nonQuestingCharacters = await FakeCharacterFactory.CreateAndStoreMultipleCharacters(10);

            var useCase = new ProcessTickUseCase(CharactersProvider);
            await useCase.Handle();

            List<Character> updatedNonQuestingCharacters = await CharactersProvider.FindAllNotQuesting();
            Assert.That(updatedNonQuestingCharacters, Is.EqualTo(nonQuestingCharacters));

            List<Character> updatedQuestingCharacters = await CharactersProvider.FindAllQuesting();
            Assert.That(questingCharacters, Is.EqualTo(questingCharacters));
            for (int i = 0; i < questingCharacters.Count; i++)
            {
                Assert.That(
                    questingCharacters[i].GetEncounterOrThrow().Id,
                    Is.Not.EqualTo(updatedQuestingCharacters[i].GetEncounterOrThrow().Id));
            }
        }
    }
}