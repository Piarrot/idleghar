using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.UseCases.System
{
    public class ProcessTickUseCase
    {
        IStorageProvider CharactersProviders;

        public ProcessTickUseCase(IStorageProvider charactersProviders)
        {
            this.CharactersProviders = charactersProviders;
        }

        public async Task Handle()
        {
            var characters = await CharactersProviders.FindAllCharactersQuesting();
            foreach (Character character in characters)
            {
                var questState = character.GetQuestStateOrThrow();
                questState.ProcessTick();
                await CharactersProviders.SaveCharacter(character);
            }

        }
    }
}