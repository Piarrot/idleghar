using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.UseCases
{
    public record class AuthenticatedRequest<RequestType>(User CurrentUser, RequestType Request);
}