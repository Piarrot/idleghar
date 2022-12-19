namespace IdlegharDotnetDomain;
public class User
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public bool EmailValidated { get; set; }
    public string EmailValidationCode { get; set; }
}