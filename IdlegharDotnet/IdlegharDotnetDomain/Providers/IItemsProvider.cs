using IdlegharDotnetDomain.Entities.Items;

namespace IdlegharDotnetDomain.Providers
{
    public interface IItemsProvider
    {
        Task<T?> FindById<T>(string id) where T : Item;
        Task Save(Item weapon);
    }
}