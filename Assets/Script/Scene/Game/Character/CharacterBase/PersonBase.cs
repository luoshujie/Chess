using System.Collections.Generic;
using Script.Scene.Game.Character.CharacterData;
using Script.Scene.Game.Manager;
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
            FightMgr.instance.refreshAttackEvent.AddListener(Attack);
        }

        public virtual void AddTargetPersonBase()
        {
            float distance;
            TargetPersonBase=FightMgr.instance.GetEnemyAtMinDistance(this, out distance);
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

                Debug.Log(personData.CurrLife);
                if (personData.CurrLife <=0)
                {
                    personData.CurrLife = 0;
                    SetAliveState(false);
                }
            }
        }

        public virtual void SetAliveState(bool state)
        {
            personData.Alive = state;
            FightMgr.instance.refreshAttackEvent.RemoveListener(Attack);
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
            CurrAttackTime -= time/1000;
            if (CurrAttackTime > 0) return;
            Debug.Log("attack");
            CurrAttackTime = personData.AttackSpeed;
            if (TargetPersonBase == null || !TargetPersonBase.personData.Alive)
            {
                AddTargetPersonBase();
            }
            if (TargetPersonBase == null||!personData.Alive||!TargetPersonBase.personData.Alive) return;
            FightMgr.instance.Hurt(this,TargetPersonBase);
            Debug.Log("attacking");
        }
    }
}