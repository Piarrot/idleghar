using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.MockProviders
{
    public class MockItemsProvider : IItemsProvider
    {
        Dictionary<string, Item> itemsById = new();
        public async Task<T?> FindById<T>(string id) where T : Item
        {
            await Task.Yield();
            Item? item;
            this.itemsById.TryGetValue(id, out item);
            return item as T;
        }

        public async Task Save(Item item)
        {
            await Task.Yield();
            var clonedItem = (Item)TestUtils.DeepClone(item);
            this.itemsById[item.Id] = clonedItem;
        }
    }
}