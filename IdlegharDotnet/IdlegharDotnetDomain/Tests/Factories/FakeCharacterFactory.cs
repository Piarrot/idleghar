using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.Factories
{
    public class FakeCharacterFactory
    {
        private FakeQuestFactory FakeQuestFactory;
        private ICharactersProvider CharactersProvider;

        public FakeCharacterFactory(ICharactersProvider charactersProvider, FakeQuestFactory fakeQuestFactory)
        {
            this.CharactersProvider = charactersProvider;
            FakeQuestFactory = fakeQuestFactory;
        }

        public async Task<Character> CreateAndStoreCharacter()
        {
            var character = new Character
            {
                Name = Faker.Name.FullName(),
            };

            await CharactersProvider.Save(character);

            return character;
        }

        public async Task<Character> CreateAndStoreCharacterWithQuest(Quest quest)
        {
            Character character = await CreateAndStoreCharacter();
            character.StartQuest(quest);
            await CharactersProvider.Save(character);
            return character;
        }

        public async Task<Character> CreateAndStoreCharacterWithQuest()
        {
            var quest = FakeQuestFactory.CreateQuest();
            return await CreateAndStoreCharacterWithQuest(quest);
        }

        public async Task<List<Character>> CreateAndStoreMultipleCharactersWithQuests(int count)
        {
            List<Character> characters = new();

            for (int i = 0; i < count; i++)
            {
                characters.Add(await CreateAndStoreCharacterWithQuest());
            }

            return characters;
        }

        public async Task<List<Character>> CreateAndStoreMultipleCharacters(int count)
        {
            List<Character> characters = new();

            for (int i = 0; i < count; i++)
            {
                characters.Add(await CreateAndStoreCharacter());
            }

            return characters;
        }
    }
}