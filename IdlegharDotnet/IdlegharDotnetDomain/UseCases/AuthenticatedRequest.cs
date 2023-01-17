using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.UseCases
{
    public record class AuthenticatedRequest<RequestType>(PlayerCreds CurrentPlayerCreds, RequestType Request);
    public record class AuthenticatedRequest(PlayerCreds CurrentPlayerCreds);
}