namespace IdlegharDotnetDomain.Entities
{
    [Serializable()]
    public class PlayerCreds : Entity
    {
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;
        public bool EmailValidated { get; set; } = false;
        public string? EmailValidationCode { get; set; }


        public static PlayerCreds From(Player player)
        {
            return new PlayerCreds()
            {
                Email = player.Email,
                Password = player.Password,
                Username = player.Username,
                EmailValidated = player.EmailValidated,
                EmailValidationCode = player.EmailValidationCode
            };
        }
    }
}