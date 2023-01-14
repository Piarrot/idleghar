using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.UseCases.System
{
    public class ProcessTickUseCase
    {
        ICharactersProvider CharactersProviders;

        public ProcessTickUseCase(ICharactersProvider charactersProviders)
        {
            this.CharactersProviders = charactersProviders;
        }

        public async Task Handle()
        {
            var characters = await CharactersProviders.FindAllQuesting();
            foreach (Character character in characters)
            {
                var questState = character.GetQuestStateOrThrow();
                questState.ProcessTick();
                await CharactersProviders.Save(character);
            }

        }
    }
}