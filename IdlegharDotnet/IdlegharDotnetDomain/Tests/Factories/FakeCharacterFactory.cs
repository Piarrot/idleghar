using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.Tests.Factories
{
    public class FakeCharacterFactory
    {
        private FakeQuestFactory FakeQuestFactory;

        public FakeCharacterFactory(FakeQuestFactory fakeQuestFactory)
        {
            FakeQuestFactory = fakeQuestFactory;
        }

        public Character CreateCharacter()
        {
            return new Character
            {
                Name = Faker.Name.FullName(),
            };
        }

        public Character CreateCharacterWithQuest(Quest quest)
        {
            Character character = CreateCharacter();
            character.CurrentQuest = quest;
            character.CurrentEncounterState = quest.Encounters.First().GetNewState();
            return character;
        }

        public Character CreateCharacterWithQuest()
        {
            var quest = FakeQuestFactory.CreateQuest();
            return CreateCharacterWithQuest(quest);
        }
    }
}