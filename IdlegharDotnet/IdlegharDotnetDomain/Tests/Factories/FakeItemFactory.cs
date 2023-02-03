using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Tests.Factories
{
    public class FakeItemFactory
    {
        public IItemsProvider itemsProvider;

        public FakeItemFactory(IItemsProvider itemProvider)
        {
            this.itemsProvider = itemProvider;
        }

        public async Task<Equipment> CreateAndStoreWeapon(int damage)
        {
            var weapon = new Equipment()
            {
                Type = EquipmentType.Weapon,
                StatChanges = new()
                {
                    [Constants.Characters.Stat.DAMAGE] = damage
                },
                Name = Faker.Lorem.Sentence()
            };
            await itemsProvider.Save(weapon);
            return weapon;
        }
    }
}