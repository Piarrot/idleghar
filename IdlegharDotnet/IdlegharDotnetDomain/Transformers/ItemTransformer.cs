using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetShared.Items;

namespace IdlegharDotnetDomain.Transformers
{
    public class ItemTransformer : Transformer<Item, ItemViewModel>
    {
        public override ItemViewModel TransformOne(Item entity)
        {
            return new ItemViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
            };
        }
    }
}