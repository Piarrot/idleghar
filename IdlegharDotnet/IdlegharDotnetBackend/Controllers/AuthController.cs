using IdlegharDotnetBackend.Providers;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.UseCases.Auth;
using IdlegharDotnetShared;
using IdlegharDotnetShared.Auth;
using Microsoft.AspNetCore.Mvc;

namespace IdlegharDotnetBackend.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthProvider authProvider, IRepositoryAggregator repositoryAggregator, ICryptoProvider cryptoProvider, IEmailsProvider emailsProvider)
        {
            AuthProvider = authProvider;
            PlayersProvider = repositoryAggregator.PlayersProvider;
            CryptoProvider = cryptoProvider;
            EmailsProvider = emailsProvider;
        }

        public IAuthProvider AuthProvider { get; }
        public IPlayersProvider PlayersProvider { get; }
        public ICryptoProvider CryptoProvider { get; }
        public IEmailsProvider EmailsProvider { get; }

        [HttpPost("login")]
        public async Task<LoginUseCaseResponse> Login([FromBody] LoginUseCaseRequest req)
        {
            var useCase = new LoginUseCase(AuthProvider, PlayersProvider, CryptoProvider);
            return await useCase.Handle(req);
        }

        [HttpPost("register")]
        public async Task<RegisterUseCaseResponse> Register([FromBody] RegisterUseCaseRequest req)
        {
            var useCase = new RegisterUseCase(PlayersProvider, CryptoProvider, EmailsProvider);
            return await useCase.Handle(req);
        }
    }
}