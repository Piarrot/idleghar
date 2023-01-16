namespace IdlegharDotnetDomain.Transformers
{
    public abstract class Transformer<T, V>
    {
        public abstract V TransformOne(T entity);
        public virtual List<V> TransformMany(List<T> entities)
        {
            return entities.ConvertAll((quest) =>
            {
                return TransformOne(quest);
            });
        }
    }
}