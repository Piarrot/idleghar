using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Tests.Factories;
using IdlegharDotnetDomain.Tests.MockProviders;
using IdlegharDotnetDomain.UseCases;
using IdlegharDotnetDomain.UseCases.Quests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests
{
    public class BaseTests
    {
        protected IUsersProvider UsersProvider = new MockUsersProvider();
        protected ICryptoProvider CryptoProvider = new MockCryptoProvider();
        protected IAuthProvider AuthProvider = new MockAuthProvider();
        protected MockEmailsProvider EmailsProvider = new MockEmailsProvider();
        protected IRandomnessProvider RandomnessProvider = new RandomnessProvider();
        protected IQuestsProvider QuestsProvider = new MockQuestsProvider();
        protected MockTimeProvider TimeProvider = new MockTimeProvider();

        [SetUp]
        public void Setup()
        {
            UsersProvider = new MockUsersProvider();
            EmailsProvider = new MockEmailsProvider();
        }

        protected async Task<User> CreateAndStoreUser(UserFactoryOptions? opts = null)
        {
            if (opts == null)
            {
                opts = new UserFactoryOptions();
            }

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = opts.Email,
                EmailValidated = opts.EmailValidated,
                Username = opts.Username,
                Password = CryptoProvider.HashPassword(opts.Password),
                Character = opts.Character
            };
            await UsersProvider.Save(user);
            return user;
        }

        protected Character CreateCharacter(string? name = null, Quest? quest = null, Encounter? encounter = null)
        {
            return new Character
            {
                Id = Guid.NewGuid().ToString(),
                Name = name ?? "CoolCharacter",
                CurrentQuest = quest,
                CurrentEncounter = encounter,
            };
        }

        protected async Task<User> CreateAndStoreUserAndCharacter()
        {
            return await CreateAndStoreUser(new Factories.UserFactoryOptions
            {
                Character = CreateCharacter()
            });
        }

        protected async Task<List<Quest>> GetAvailableQuests(User user)
        {
            await new GetAvailableQuestsUseCase(RandomnessProvider, QuestsProvider, TimeProvider).Handle(new AuthenticatedRequest(user));
            return (await QuestsProvider.GetCurrentQuestBatch())!.Quests;
        }

        protected async Task<User> CreateAndStoreUserAndCharacterWithQuest()
        {
            var user = await CreateAndStoreUserAndCharacter();
            var quests = await GetAvailableQuests(user);
            user.Character!.CurrentQuest = quests[0];
            user.Character.CurrentEncounter = quests[0].Encounters[0];
            await UsersProvider.Save(user);

            return user;
        }
    }
}