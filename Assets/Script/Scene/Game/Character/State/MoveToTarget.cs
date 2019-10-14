using Script.FSM;
using UnityEngine;

namespace Script.Scene.Game.Character.State
{
    public class MoveToTarget : StateBase
    {
        private float Distance;
        public MoveToTarget(FsmSystem fsm,float distance)
        {
            Fsm = fsm;
            Distance = distance;
        }
        public override void CheckTransition(GameObject self, GameObject target)
        {
            if (Vector3.Distance(self.transform.position,target.transform.position)<=Distance)
            {
                Fsm.PerformTransition(Transition.Attack);
            }
        }

        public override void Act(GameObject self, GameObject target)
        {
            
        }
    }
}
