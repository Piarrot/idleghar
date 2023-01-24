using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.Factories
{
    public class FakeItemFactory
    {
        public IItemsProvider itemsProvider;

        public FakeItemFactory(IItemsProvider itemProvider)
        {
            this.itemsProvider = itemProvider;
        }

        public async Task<Weapon> CreateAndStoreWeapon(int damage)
        {
            var weapon = new Weapon()
            {
                DamageIncrease = damage,
                Name = Faker.Lorem.Sentence()
            };
            await itemsProvider.Save(weapon);
            return weapon;
        }
    }
}