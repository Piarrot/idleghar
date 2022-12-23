using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.UseCases
{
    public class AuthenticatedRequest<RequestType>
    {
        public User CurrentUser { get; set; }

        public RequestType Request { get; set; }
    }
}