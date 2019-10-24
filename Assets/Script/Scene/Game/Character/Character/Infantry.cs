using System;
using Script.Enum;
using Script.Scene.Game.Character.CharacterBase;
using Script.Scene.Game.Character.CharacterData;

namespace Script.Scene.Game.Character.Character
{
    public class Infantry : PersonBase
    {
        private void Start()
        {
            personData = new PersonData(CharacterTypeEnum.Enemy, 1, 1, 100, 1, 1, 1, 1, 10, 0, 1);
        }
    }
}