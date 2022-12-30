namespace IdlegharDotnetShared.Auth
{
    public record class LoginUseCaseRequest(string EmailOrUsername, string Password);
}