using UnityEngine;

namespace Script.FSM
{
    public class ChasePlayerState : StateBase
    {
        public ChasePlayerState()
        {
            stateID = StateID.Attack;
        }
        public override void CheckTransition(GameObject self, GameObject target)
        {
            if (Vector3.Distance(self.transform.position,target.transform.position)>30)
            {
                target.GetComponent<NpcContorl>().SetTransition(Transition.MoveToTarget);
            }
        }

        public override void Act(GameObject self, GameObject target)
        {
            Rigidbody rb = target.GetComponent<Rigidbody>();
            Vector3 vel = rb.velocity;
            Vector3 moveDir = self.transform.position - target.transform.position;
            
            target.transform.rotation = Quaternion.Slerp(target.transform.rotation,Quaternion.LookRotation(moveDir), 5*Time.deltaTime);
            target.transform.eulerAngles=new Vector3(0,target.transform.eulerAngles.y,0);
            vel = moveDir.normalized * 10;
            rb.velocity = vel;
        }
    }
}
