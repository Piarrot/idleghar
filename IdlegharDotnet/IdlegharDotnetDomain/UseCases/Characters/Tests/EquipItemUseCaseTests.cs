using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Characters.Tests
{
    public class EquipItemUseCaseTests : BaseTests
    {
        [Test]
        public async Task CharacterCanEquipItems()
        {
            Player player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();

            Weapon weapon = await FakeItemFactory.CreateAndStoreWeapon(5);
            Weapon weapon2 = await FakeItemFactory.CreateAndStoreWeapon(10);

            player.Items.Add(weapon);
            player.Items.Add(weapon2);

            await PlayersProvider.Save(player);

            var useCase = new EquipItemUseCase(PlayersProvider, ItemsProvider);
            await useCase.Handle(new(player, new() { ItemId = weapon.Id }));


            var updatedPlayer = await PlayersProvider.FindById(player.Id);
            Character character = updatedPlayer!.Character!;

            Assert.That(character.Inventory.Weapon, Is.EqualTo(weapon));
            Assert.That(character.Damage, Is.EqualTo(11));
            Assert.That(character.Owner.Items.Contains(weapon), Is.False);
            Assert.That(character.Owner.Items.Contains(weapon2), Is.True);

            await useCase.Handle(new(player, new() { ItemId = weapon2.Id }));

            updatedPlayer = await PlayersProvider.FindById(player.Id);
            character = updatedPlayer!.Character!;
            Assert.That(character.Inventory.Weapon, Is.EqualTo(weapon2));
            Assert.That(character.Damage, Is.EqualTo(16));
            Assert.That(character.Owner.Items.Contains(weapon2), Is.False);
            Assert.That(character.Owner.Items.Contains(weapon), Is.True);
        }

        [Test]
        public async Task CharacterCannotEquipItemsWhileQuesting()
        {
            Player player = await FakePlayerFactory.CreateAndStorePlayerAndCharacterWithQuest();
            Character character = player.Character!;
            Weapon weapon = await FakeItemFactory.CreateAndStoreWeapon(5);
            player.Items.Add(weapon);
            await PlayersProvider.Save(player);

            var useCase = new EquipItemUseCase(PlayersProvider, ItemsProvider);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new(player, new() { ItemId = weapon.Id }));
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.CHARACTER_ALREADY_QUESTING));
        }

        [Test]
        public async Task CharacterCannotEquipUnownedItems()
        {
            Player player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();
            Character character = player.Character!;
            Weapon weapon = await FakeItemFactory.CreateAndStoreWeapon(5);

            var useCase = new EquipItemUseCase(PlayersProvider, ItemsProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new(player, new() { ItemId = weapon.Id }));
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.ITEM_NOT_OWNED));
        }
    }
}