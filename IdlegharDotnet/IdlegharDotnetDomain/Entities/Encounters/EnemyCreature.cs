namespace IdlegharDotnetDomain.Entities.Encounters
{
    [Serializable()]
    public class EnemyCreature
    {
        public EnemyCreature(string name, string description, int hp, int damage)
        {
            this.Name = name;
            this.Description = description;
            this.HP = hp;
            this.Damage = damage;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int HP { get; private set; }
        public int Damage { get; private set; }

        public void ReceiveDamage(int damage)
        {
            this.HP -= damage;
        }

        internal EnemyCreature Clone()
        {
            return new EnemyCreature(Name, Description, HP, Damage);
        }
    }
}