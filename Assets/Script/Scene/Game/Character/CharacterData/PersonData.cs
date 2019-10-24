using System.Collections.Generic;
using Script.Enum;
using Script.Scene.Game.Character.CharacterBase;

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
        public float AttackDistance;

        public bool Alive;

        public List<BuffConfig> BuffConfigList = new List<BuffConfig>();

        public PersonData(CharacterTypeEnum characterTypeEnum, int id, int level, int maxLife, int attack, int defense,
            int moveSpeed,
            int attackSpeed, int maxEnergy, int currEnergy, float attackDistance)
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
            AttackDistance = attackDistance;
            Alive = true;
        }

        public PersonData()
        {
        }
    }
}