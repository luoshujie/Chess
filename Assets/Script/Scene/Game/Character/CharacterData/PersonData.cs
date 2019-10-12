using Script.Enum;

namespace Script.Scene.Game.Character.CharacterData
{
    public class PersonData
    {
        public CharacterTypeEnum characterTypeEnum;
        public int Id;
        public int Level;
        public int MaxLife;
        public int CurrLife;
        public int Attack;
        public int Defense;
        public int MoveSpeed;
        public int AttackSpeed;
        public int MaxEnergy;
        public int CurrEnergy;

        public bool Alive;

        public PersonData(CharacterTypeEnum characterTypeEnum,int id, int level, int maxLife, int attack, int defense, int moveSpeed,
            int attackSpeed, int maxEnergy, int currEnergy)
        {
            this.characterTypeEnum = characterTypeEnum;
            Id = id;
            Level = level;
            MaxLife = maxLife;
            CurrLife = maxLife;
            Attack = attack;
            Defense = defense;
            MoveSpeed = moveSpeed;
            AttackSpeed = attackSpeed;
            MaxEnergy = maxEnergy;
            CurrEnergy = currEnergy;
            Alive = true;
        }

        public PersonData()
        {
        }
    }
}