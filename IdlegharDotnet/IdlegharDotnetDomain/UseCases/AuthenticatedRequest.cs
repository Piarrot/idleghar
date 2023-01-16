using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.UseCases
{
    public record class AuthenticatedRequest<RequestType>(Player CurrentPlayer, RequestType Request);
    public record class AuthenticatedRequest(Player CurrentPlayer);
}