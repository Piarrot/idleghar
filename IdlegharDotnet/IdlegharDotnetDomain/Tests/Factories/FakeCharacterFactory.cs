using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.Factories
{
    public class FakeCharacterFactory
    {
        public IStorageProvider StorageProvider { get; }
        public FakeQuestFactory FakeQuestFactory { get; }

        public FakeCharacterFactory(IStorageProvider playersProvider, FakeQuestFactory fakeQuestFactory)
        {
            StorageProvider = playersProvider;
            FakeQuestFactory = fakeQuestFactory;
        }

        public async Task<Character> CreateAndStoreCharacter(Player? player = null)
        {
            if (player == null)
            {
                player = new Player();
            }
            var character = new Character(player)
            {
                Name = Faker.Name.FullName(),
            };

            player.Character = character;

            await StorageProvider.SavePlayer(character.Owner);

            return character;
        }

        public async Task<Character> CreateAndStoreCharacterWithQuest(Quest quest, Player? player = null)
        {
            Character character = await CreateAndStoreCharacter(player);
            character.StartQuest(quest);
            await StorageProvider.SavePlayer(character.Owner);
            return character;
        }

        public async Task<Character> CreateAndStoreCharacterWithQuest(Player? player = null)
        {
            var quest = FakeQuestFactory.CreateQuest();
            return await CreateAndStoreCharacterWithQuest(quest, player);
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