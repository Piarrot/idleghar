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
    }
}