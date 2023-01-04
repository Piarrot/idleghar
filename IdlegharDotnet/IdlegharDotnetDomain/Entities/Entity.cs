namespace IdlegharDotnetDomain.Entities
{
    public abstract class Entity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals(obj as Entity);
        }

        public bool Equals(Entity? entity)
        {
            return entity != null && entity.Id == this.Id;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}