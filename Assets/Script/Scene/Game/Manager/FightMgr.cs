using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Script.Enum;
using Script.Event;
using Script.Scene.Game.Character.CharacterBase;
using Script.Scene.Game.Character.CharacterData;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Scene.Game.Manager
{
    public class FightMgr : MonoBehaviour
    {
        public static FightMgr instance;

        public RefreshAttackEvent refreshAttackEvent = new RefreshAttackEvent();
        public List<CharacterPos> PlayerPosList;
        public List<CharacterPos> EnemyPosList;

        private List<PersonBase> PlayerPersonList = new List<PersonBase>();
        private List<PersonBase> EnemyPersonList = new List<PersonBase>();

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                StartGame();
            }
        }

        private void Start()
        {
            for (int i = 0; i < 11; i++)
            {
                GameObject PlayerPosObj = Instantiate(PlayerPosList[PlayerPosList.Count - 1].gameObject);
                PlayerPosList.Add(PlayerPosObj.GetComponent<CharacterPos>());
                if (i != 5)
                {
                    PlayerPosObj.transform.position += PlayerPosObj.transform.right * 1.1f;
                }
                else
                {
                    PlayerPosObj.transform.position =
                        PlayerPosList[0].transform.position - PlayerPosList[0].transform.forward * 1.1f;
                }

                GameObject EnemyPosObj = Instantiate(EnemyPosList[EnemyPosList.Count - 1].gameObject);
                EnemyPosList.Add(EnemyPosObj.GetComponent<CharacterPos>());
                if (i != 5)
                {
                    EnemyPosObj.transform.position += EnemyPosObj.transform.right * 1.1f;
                }
                else
                {
                    EnemyPosObj.transform.position =
                        EnemyPosList[0].transform.position - EnemyPosList[0].transform.forward * 1.1f;
                }
            }
        }

        /// <summary>
        /// 获取最短距离的敌人
        /// </summary>
        /// <param name="self"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public PersonBase GetEnemyAtMinDistance(PersonBase self, [CanBeNull] out float distance)
        {
            float minDistance = 99999;
            distance = minDistance;
            int index = 0;
            if (self.personData.characterTypeEnum == CharacterTypeEnum.Player)
            {
                if (EnemyPersonList.Count <= 0) return null;
                for (int i = 0; i < EnemyPersonList.Count; i++)
                {
                    distance = Vector3.Distance(self.transform.position, EnemyPersonList[i].transform.position);
                    if (distance < minDistance)
                    {
                        distance = minDistance;
                        index = i;
                    }
                }

                return EnemyPersonList[index];
            }
            else if (self.personData.characterTypeEnum == CharacterTypeEnum.Enemy)
            {
                if (PlayerPersonList.Count <= 0) return null;
                for (int i = 0; i < PlayerPersonList.Count; i++)
                {
                    distance = Vector3.Distance(self.transform.position,
                        PlayerPersonList[i].transform.position);
                    if (distance < minDistance)
                    {
                        distance = minDistance;
                        index = i;
                    }
                }

                return PlayerPersonList[index];
            }

            return null;
        }

        public void Hurt(PersonBase self, PersonBase target)
        {
            target.Hurt(self.personData.Attack);
        }

        public BuffConfig AllBuff(PersonData personData)
        {
            BuffConfig buffConfig = new BuffConfig();
            return buffConfig;
        }

        public void StartGame()
        {
            for (int i = 0; i < PlayerPosList.Count; i++)
            {
                if (PlayerPosList[i].personBase != null)
                {
                    PlayerPersonList.Add(PlayerPosList[i].personBase);
                }

                if (EnemyPosList[i].personBase != null)
                {
                    EnemyPersonList.Add(EnemyPosList[i].personBase);
                }
            }

            RefreshMgr.instance.AddTimeTask(() => { RefreshAttack(); }, 100, TimeUnitEnum.Millisecond, -1);
        }

        public void RefreshAttack()
        {
            refreshAttackEvent.Invoke(100);
        }

        public void RemovePerson(PersonBase personBase)
        {
            if (personBase.personData.characterTypeEnum == CharacterTypeEnum.Player)
            {
                PlayerPersonList.Remove(personBase);
            }
            else if (personBase.personData.characterTypeEnum == CharacterTypeEnum.Enemy)
            {
                EnemyPersonList.Remove(personBase);
            }
        }
    }
}