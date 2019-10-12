using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Scene.Game.Manager
{
    public class FightMgr : MonoBehaviour
    {
        public static FightMgr instance;

        public List<GameObject> PlayerPosList;
        public List<GameObject> EnemyPosList;

        private void Awake()
        {
            instance = this;
        }
    }
}