using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Characters;

namespace IdlegharDotnetDomain.UseCases.Characters
{
    public class EquipItemUseCase
    {
        IStorageProvider StorageProvider;

        public EquipItemUseCase(IStorageProvider playersProvider)
        {
            StorageProvider = playersProvider;
        }

        public async Task Handle(AuthenticatedRequest<EquipItemUseCaseRequest> authRequest)
        {
            var player = await StorageProvider.GetPlayerByIdOrThrow(authRequest.CurrentPlayerCreds.Id);
            var equipment = player.Items.Find((p) => p.Id == authRequest.Request.ItemId) as Equipment;
            if (equipment == null) throw new ArgumentException(Constants.ErrorMessages.INVALID_ITEM);

            var character = player!.Character;
            if (character == null) throw new ArgumentException(Constants.ErrorMessages.CHARACTER_NOT_CREATED);
            character.ThrowIfQuesting();

            player.Items.Remove(equipment);
            var oldEquipment = character.EquipItem(equipment);
            if (oldEquipment != null)
            {
                player.Items.Add(oldEquipment);
            }
            await StorageProvider.SavePlayer(player);
        }
    }
}