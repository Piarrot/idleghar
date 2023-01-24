using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Characters;

namespace IdlegharDotnetDomain.UseCases.Characters
{
    public class EquipItemUseCase
    {
        IPlayersProvider PlayersProvider;
        IItemsProvider ItemsProvider;

        public EquipItemUseCase(IPlayersProvider playersProvider, IItemsProvider itemsProvider)
        {
            PlayersProvider = playersProvider;
            ItemsProvider = itemsProvider;
        }

        public async Task Handle(AuthenticatedRequest<EquipItemUseCaseRequest> authRequest)
        {
            var player = await PlayersProvider.FindById(authRequest.CurrentPlayerCreds.Id);
            var equipment = await ItemsProvider.FindById<Equipment>(authRequest.Request.ItemId);

            if (equipment == null) throw new ArgumentException(Constants.ErrorMessages.INVALID_ITEM);

            var character = player!.Character;
            if (character == null) throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_CREATED);
            character.ThrowIfQuesting();

            if (!player.Items.Remove(equipment)) throw new InvalidOperationException(Constants.ErrorMessages.ITEM_NOT_OWNED);
            var oldEquipment = character.EquipItem(equipment);
            if (oldEquipment != null)
            {
                player.Items.Add(oldEquipment);
            }
        }
    }
}