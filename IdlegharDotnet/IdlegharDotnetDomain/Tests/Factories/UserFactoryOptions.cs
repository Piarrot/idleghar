using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.Tests.Factories
{
    public class UserFactoryOptions
    {
        public string Email = "coolguy69@email.com";
        public string Password = "user1234";
        public bool EmailValidated = true;
        public string Username = "CoolGuy69";

        public Character? Character;
    }
}