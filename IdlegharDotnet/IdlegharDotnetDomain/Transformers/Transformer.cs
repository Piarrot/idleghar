namespace IdlegharDotnetDomain.Transformers
{
    public abstract class Transformer<T, V> where T : class where V : class
    {
        public abstract V TransformOne(T entity);
        public virtual V? TransformOneOptional(T? entity)
        {
            if (entity == null) return null;
            return TransformOne(entity);
        }
        public virtual List<V> TransformMany(List<T> entities)
        {
            return entities.ConvertAll((quest) =>
            {
                return TransformOne(quest);
            });
        }
    }
}