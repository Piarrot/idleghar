namespace IdlegharDotnetShared.Auth
{
    public record class RegisterUseCaseRequest(string Email, string Password, string Username);
}