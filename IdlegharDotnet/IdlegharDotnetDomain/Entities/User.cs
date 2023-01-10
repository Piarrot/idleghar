namespace IdlegharDotnetDomain.Entities
{
    public class User : Entity
    {
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;
        public bool EmailValidated { get; set; } = false;
        public string? EmailValidationCode { get; set; }
        public Character? Character { get; set; }

        public bool HasCharacter => Character != null;

        public Character GetCharacterOrThrow()
        {
            if (Character == null)
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_CREATED);
            return Character;
        }
    }
}