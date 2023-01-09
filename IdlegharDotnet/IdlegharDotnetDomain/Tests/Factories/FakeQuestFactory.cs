using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Entities.Encounters;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.UseCases;
using IdlegharDotnetDomain.UseCases.Quests;

namespace IdlegharDotnetDomain.Tests.Factories
{
    public class FakeQuestFactory
    {
        private IRandomnessProvider RandomnessProvider;
        private IQuestsProvider QuestsProvider;
        private ITimeProvider TimeProvider;

        public FakeQuestFactory(IRandomnessProvider randomnessProvider, IQuestsProvider questsProvider, ITimeProvider timeProvider)
        {
            RandomnessProvider = randomnessProvider;
            QuestsProvider = questsProvider;
            TimeProvider = timeProvider;
        }

        public async Task<List<Quest>> GetAvailableQuests(User user)
        {
            await new GetAvailableQuestsUseCase(RandomnessProvider, QuestsProvider, TimeProvider).Handle(new AuthenticatedRequest(user));
            return (await QuestsProvider.GetCurrentQuestBatch())!.Quests;
        }

        public Quest CreateQuest(string difficulty, List<Encounter> encounters)
        {
            return new Quest
            {
                BatchId = Guid.NewGuid().ToString(),
                Name = Faker.Lorem.Sentence(),
                Difficulty = difficulty,
                Encounters = encounters,
            };
        }

        public Quest CreateQuest(List<Encounter> encounters)
        {
            return CreateQuest(Constants.Difficulties.EASY, encounters);
        }

        public Quest CreateQuest()
        {
            return CreateQuest(CreateEncounters());
        }

        public List<Encounter> CreateEncounters()
        {
            return new List<Encounter>(){
                CreateEncounter(),
                CreateEncounter(),
                CreateEncounter()
            };
        }

        public Encounter CreateEncounter()
        {
            return new CombatEncounter();
        }

    }
}