using IdlegharDotnetShared.SharedConstants;

namespace IdlegharDotnetDomain.Entities.Items
{
    [Serializable()]
    public abstract class Item : Entity
    {
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public ItemQuality Quality { get; set; } = ItemQuality.Common;

    }
}