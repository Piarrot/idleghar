using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetShared.SharedConstants;

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
                    [CharacterStat.DAMAGE] = damage
                },
                Name = Faker.Lorem.Sentence()
            };
            return weapon;
        }
    }
}