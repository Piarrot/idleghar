namespace IdlegharDotnetShared.Characters
{
    public class CharacterViewModel : EntityViewModel
    {
        public string Name { get; set; } = String.Empty;
        public int Level { get; set; } = 1;
        public int XP { get; set; } = 0;
        public int HP { get; set; } = 1;
        public int MaxHP { get; set; } = 1;
        public int Damage { get; set; } = 1;
        public int Toughness { get; set; } = 1;
    }
}