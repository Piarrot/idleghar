using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Tests.Factories
{
    public class FakeItemFactory
    {

        public Equipment CreateWeapon(int damage)
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
            return weapon;
        }
    }
}