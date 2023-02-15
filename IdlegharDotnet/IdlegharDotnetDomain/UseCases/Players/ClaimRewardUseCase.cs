using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Players;

namespace IdlegharDotnetDomain.UseCases.Players
{
    public class ClaimRewardUseCase
    {
        IPlayersProvider PlayersProvider;

        public ClaimRewardUseCase(IPlayersProvider playersProvider)
        {
            PlayersProvider = playersProvider;
        }

        public async Task Handle(AuthenticatedRequest<ClaimRewardUseCaseRequest> request)
        {
            var player = await this.PlayersProvider.GetByIdOrThrow(request.CurrentPlayerCreds.Id);
            if (player.Character == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.INVALID_PLAYER);
            }

            var reward = player.UnclaimedRewards.Find((r) => r.Id == request.Request.RewardId);
            if (reward == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.INVALID_REWARD);
            }
            player.ClaimReward(reward);
            await PlayersProvider.Save(player);
        }
    }
}