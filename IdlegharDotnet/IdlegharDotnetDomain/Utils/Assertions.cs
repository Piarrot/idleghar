using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.Utils
{
    public static class Assertions
    {
        public static void UserHasCharacter(User user)
        {
            if (!user.HasCharacter)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_CREATED);
            }
        }

        public static void AssertCharacterIsQuesting(Character character)
        {
            if (!character.IsQuesting)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_QUESTING);
            }
        }

        internal static void CharacterIsNotQuesting(Character character)
        {
            if (character.IsQuesting)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_ALREADY_QUESTING);
            }
        }
    }

}