using UnityEngine;

namespace Script.Scene.Game.Character.CharacterBase
{
    public class PersonData
    {
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

        public PersonData(int id, int level, int maxLife, int currLife, int attack, int defense, int moveSpeed,
            int attackSpeed, int maxEnergy, int currEnergy)
        {
            Id = id;
            Level = level;
            MaxLife = maxLife;
            CurrLife = currLife;
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