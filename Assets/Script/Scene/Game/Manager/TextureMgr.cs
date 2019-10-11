using System;
using UnityEngine;

namespace Script.Scene.Game.Manager
{
    public class TextureMgr : MonoBehaviour
    {
        public static TextureMgr instance;

        private void Awake()
        {
            instance = this;
        }

        public Sprite GetHeroHeadImgAtId(int id)
        {
            Sprite sprite = Resources.Load<Sprite>("Texture/HeroHead/" + id);
            return sprite;
        }
    }
}