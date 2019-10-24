using System.Collections.Generic;
using Script.FSM;
using Script.Scene.Game.Character.CharacterData;
using Script.Scene.Game.Manager;
using UnityEngine;

namespace Script.Scene.Game.Character.CharacterBase
{
    public class PersonBase : MonoBehaviour
    {
        public PersonData personData;

        public float CurrAttackTime = 0;

        public virtual void Init(PersonData personData)
        {
            this.personData = personData;
            FightMgr.instance.refreshAttackEvent.AddListener(AttackCooling);
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

                Debug.Log(personData.CurrLife);
                if (personData.CurrLife <= 0)
                {
                    personData.CurrLife = 0;
                    SetAliveState(false);
                }
            }
        }

        public virtual void SetAliveState(bool state)
        {
            personData.Alive = state;
            FightMgr.instance.refreshAttackEvent.RemoveListener(AttackCooling);
            FightMgr.instance.RemovePerson(this);
            Destroy(gameObject);
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
                personData.BuffConfigList.Add(buffConfig);
            }
        }

        public virtual void CutBuff(BuffConfig buffConfig)
        {
            if (personData.BuffConfigList.Contains(buffConfig))
            {
                personData.BuffConfigList.Remove(buffConfig);
            }
        }

        public virtual void AttackCooling(float time)
        {
            if (CurrAttackTime <= 0) return;
            CurrAttackTime -= time / 1000;
        }
    }
}