using IdlegharDotnetDomain.Factories;
using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Constants;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Transformers.Tests
{
    public class CharacterTransformerTests : BaseTests
    {
        [Test]
        public async Task TestCharacterTransformer()
        {
            var encounterFactory = new CombatEncounterFactory(RandomnessProviderMock);
            var quest = FakeQuestFactory.CreateQuest(Difficulty.EASY, new(){
                encounterFactory.CreateCombat(Difficulty.EASY),
                encounterFactory.CreateCombat(Difficulty.EASY)
            });

            var character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(quest);
            character.CurrentQuestState!.ProcessTick();

            CharacterTransformer transformer = new();

            var viewModel = transformer.TransformOne(character);
            while (character.IsQuesting)
            {

                Assert.That(viewModel.Id, Is.EqualTo(character.Id));
                Assert.That(viewModel.Name, Is.EqualTo(character.Name));
                Assert.That(viewModel.CurrentQuestState!.Quest.Id, Is.EqualTo(character.CurrentQuestState!.Quest.Id));
                Assert.That(viewModel.CurrentQuestState.Quest.Difficulty, Is.EqualTo(character.CurrentQuestState.Quest.Difficulty));

                Assert.That(viewModel.CurrentQuestState.Progress, Is.EqualTo(character.CurrentQuestState.Progress));
                Assert.That(viewModel.CurrentQuestState.PreviousEncounters.Count, Is.EqualTo(character.CurrentQuestState.Previous.Count));

                character.CurrentQuestState!.ProcessTick();
                viewModel = transformer.TransformOne(character);
            }

            viewModel = transformer.TransformOne(character);

            Assert.That(viewModel.QuestHistory.Count, Is.EqualTo(character.QuestHistory.Count));
            Assert.That(viewModel.QuestHistory[0].Quest.Id, Is.EqualTo(character.QuestHistory[0].Quest.Id));

        }
    }
}