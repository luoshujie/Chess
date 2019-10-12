using System;
using Script.Enum;
using Script.Scene.Game.Character.CharacterBase;
using Script.Scene.Game.Character.CharacterData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Scene.Game
{
    public class CharacterPos : MonoBehaviour
    {
        public PersonBase personBase;

        private void Start()
        {
            if (Random.Range(0,2)==0)
            {
                Invoke("AddModel",3);
            }
           
        }

        private void AddModel()
        {
            if (gameObject.tag == "EnemyPos")
            {
                PersonData personData=new PersonData(CharacterTypeEnum.Enemy,1,10,10,2,1,1,1,100,100);
                InitChessPos(personData);
            }
        }

        public void InitChessPos(PersonData personData)
        {
            if (personData == null||personBase) return;
            GameObject model=Resources.Load<GameObject>("Prefab/" + GameConfig.HeroModelPath[personData.Id]);
            GameObject personModel=Instantiate(model);
            personModel.transform.position = transform.position;
            personBase= personModel.GetComponent<PersonBase>();
            personBase.Init(personData);
        }
    }
}
