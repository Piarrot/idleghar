namespace IdlegharDotnetDomain.Entities
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public bool EmailValidated { get; set; }
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