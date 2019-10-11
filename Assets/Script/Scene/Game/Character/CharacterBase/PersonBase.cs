using System.Collections.Generic;
using UnityEngine;

namespace Script.Scene.Game.Character.CharacterBase
{
    public class PersonBase : MonoBehaviour
    {
        public PersonData personData;

        public List<BuffConfig> BuffConfigList = new List<BuffConfig>();

        public PersonBase TargetPersonBase;

        public float CurrAttackTime;

        public virtual void Init(PersonData personData)
        {
            this.personData = personData;
            CurrAttackTime = personData.AttackSpeed;
        }

        public virtual void AddTargetPersonBase(PersonBase targetPersonBase)
        {
            this.TargetPersonBase = targetPersonBase;
        }

        public virtual void CutTargetPersonBase()
        {
            TargetPersonBase = null;
        }

        public virtual void Hurt(int value)
        {
            ChangeLife(-value);
        }

        public virtual void RecoveryLife(int value)
        {
            ChangeLife(value);
        }

        public virtual void ChangeLife(int value)
        {
            if (personData.Alive)
            {
                personData.CurrLife += value;
                if (personData.CurrLife > personData.MaxLife) personData.CurrLife = personData.MaxLife;

                if (personData.CurrLife < 0)
                {
                    personData.CurrLife = 0;
                    SetAliveState(false);
                }
            }
        }

        public virtual void SetAliveState(bool state)
        {
            personData.Alive = state;
        }

        public virtual void AddEnergy(int value)
        {
            ChangeEnergy(value);
        }

        public virtual void CutEnergy(int value)
        {
            ChangeLife(-value);
        }

        public virtual void ChangeEnergy(int value)
        {
            if (personData.Alive)
            {
                personData.CurrEnergy += value;
                if (personData.CurrEnergy > personData.MaxEnergy) personData.CurrEnergy = personData.MaxEnergy;

                if (personData.CurrEnergy < 0) personData.CurrEnergy = 0;
            }
        }

        public virtual void AddBuff(BuffConfig buffConfig)
        {
            if (personData.Alive)
            {
                BuffConfigList.Add(buffConfig);
            }
        }

        public virtual void CutBuff(BuffConfig buffConfig)
        {
            if (BuffConfigList.Contains(buffConfig))
            {
                BuffConfigList.Remove(buffConfig);
            }
        }

        public virtual void Attack(float time)
        {
            CurrAttackTime -= time;
            if (CurrAttackTime > 0) return;
            if (TargetPersonBase == null) return;
        }
    }
}