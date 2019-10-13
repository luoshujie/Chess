using System;
using UnityEngine;

namespace Script.FSM
{
    public class NpcContorl : MonoBehaviour
    {
        public GameObject player;
        public Transform[] paths;
        private FsmSystem fsm;

        private void Start()
        {
            MaskFSM();
        }

        //设置状态机状态
        public void SetTransition(Transition trans)
        {
            fsm.PerformTransition(trans);
        }

        private void FixedUpdate()
        {
            fsm.CurrentState.CheckTransition(player,this.gameObject);
            fsm.CurrentState.Act(player,this.gameObject);
        }

        private void MaskFSM()
        {
            FollowPathState followPathState=new FollowPathState(paths);
            followPathState.AddTransition(Transition.SamPlayer,StateID.ChasingPlayer);
            ChasePlayerState chasePlayerState =new ChasePlayerState();
            chasePlayerState.AddTransition(Transition.LostPlayer,StateID.FollowingPath);
            fsm = new FsmSystem();
            fsm.AddState(followPathState);
            fsm.AddState(chasePlayerState);
        }
    }
}
