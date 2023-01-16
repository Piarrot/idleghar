namespace IdlegharDotnetDomain.Entities
{
    [Serializable()]
    public class Player : Entity
    {
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;
        public bool EmailValidated { get; set; } = false;
        public string? EmailValidationCode { get; set; }
    }
}