using Script.Scene.Game.Character.CharacterBase;
using UnityEngine;

namespace Script.Scene.Game
{
    public class CharacterPos : MonoBehaviour
    {
        public PersonBase personBase;
        public void InitChessPos(PersonData personData)
        {
            if (personData == null) return;
            GameObject model=Resources.Load<GameObject>("Prefab/" + GameConfig.HeroModelPath[personData.Id]);
            GameObject personModel=Instantiate(model);
            personModel.transform.position = transform.position;
            personBase= personModel.GetComponent<PersonBase>();
            personBase.Init(personData);
        }
    }
}
