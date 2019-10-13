using System.Collections.Generic;
using UnityEngine;

namespace Script.FSM
{
    public abstract class StateBase
    {
        protected StateID stateID;

        public StateID ID
        {
            get => stateID;
            set => stateID = value;
        }

        public Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();

        /// <summary>
        /// 添加转换
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="stateId"></param>
        public void AddTransition(Transition trans, StateID stateId)
        {
            if (trans==Transition.NullTransition)
            {
                Debug.LogError("NullTransition");
                return;
            }

            if (stateId==StateID.NullStateID)
            {
                Debug.LogError("NullStateID");
                return;
            }

            if (map.ContainsKey(trans))
            {
                Debug.LogError("Trans is over");
                return;
            }
            map.Add(trans,stateId);
        }

        /// <summary>
        /// 删除转换
        /// </summary>
        /// <param name="trans"></param>
        public void DeleteTransition(Transition trans)
        {
            if (trans == Transition.NullTransition)
            {
                Debug.LogError("NullTransition");
                return;
            }

            if (map.ContainsKey(trans))
            {
                map.Remove(trans);
                return;
            }
            Debug.LogError("trans is not on map");
        }

        //根据转换条件返回状态ID
        public StateID GetOutputState(Transition trans)
        {
            if (map.ContainsKey(trans))
            {
                return map[trans];
            }

            return StateID.NullStateID;
        }
        
        /// <summary>
        /// 用于进入状态前，设置进入状态的条件
        /// 在进入当前状态之前，FSM系统会自动调用
        /// </summary>
        public virtual void DoBeforeEntering(){}
        
        /// <summary>
        /// 用于离开状态时的变量重置
        /// 在更改为新状态之前，FSM系统会自动调用
        /// </summary>
        public virtual void DoBeforeLeaving(){}

        /// <summary>
        /// 用于判断是否可以转换到另一种状态，每侦都会执行
        /// </summary>
        /// <param name="player"></param>
        /// <param name="npc"></param>
        public abstract void CheckTransition(GameObject player, GameObject npc);

        /// <summary>
        /// 控制npc的行为，每侦都会执行
        /// </summary>
        /// <param name="player"></param>
        /// <param name="npc"></param>
        public abstract void Act(GameObject player,GameObject npc);
    }
}