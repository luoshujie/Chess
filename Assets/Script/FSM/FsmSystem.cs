using System.Collections.Generic;
using UnityEngine;

namespace Script.FSM
{
    public class FsmSystem
    {
        private List<StateBase> StateList;
        //改变FSM状态的唯一方式是触发转换
        //不要直接改变状态
        private StateID currentStateID;

        public StateID CurrentStateID
        {
            get => currentStateID;
        }

        private StateBase currentState;

        public StateBase CurrentState
        {
            get => currentState;
        }

        public FsmSystem()
        {
            StateList=new List<StateBase>();
        }

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state"></param>
        public void AddState(StateBase state)
        {
            if (state==null)
            {
                Debug.LogError("state is null");
                return;
            }

            if (StateList.Count==0)
            {
                StateList.Add(state);
                currentState = state;
                currentStateID = state.ID;
                return;
            }

            foreach (StateBase s in StateList)
            {
                if (s.ID==state.ID)
                {
                    Debug.LogError("已经存在这个状态"+s);
                    return;
                }
            }
            StateList.Add(state);
        }

        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="id"></param>
        public void DeleteState(StateID id)
        {
            if (id==StateID.NullStateID)
            {
                Debug.LogError("id id nullStateID");
                return;
            }

            foreach (StateBase s in StateList)
            {
                if (s.ID==id)
                {
                    StateList.Remove(s);
                    return;
                }
            }
            Debug.LogError("不存在这个状态");
        }

        public void PerformTransition(Transition trans)
        {
            if (trans==Transition.NullTransition)
            {
                Debug.LogError("trans is nullTransition");
                return;
            }
            //获取转换对应得状态ID
            StateID stateId = currentState.GetOutputState(trans);
            if (stateId==StateID.NullStateID)
            {
                Debug.LogError("id is nullStateID");
                return;
            }

            //更新当前的状态ID
            currentStateID = stateId;
            foreach (StateBase state in StateList)
            {
                if (state.ID==currentStateID)
                {
                    currentState.DoBeforeLeaving();
                    currentState = state;
                    currentState.DoBeforeEntering();
                    break;
                }
            }
        }
    }
}
