using UnityEngine;

namespace Script.FSM
{
    public class ChasePlayerState : StateBase
    {
        public ChasePlayerState()
        {
            stateID = StateID.ChasingPlayer;
        }
        public override void CheckTransition(GameObject player, GameObject npc)
        {
            if (Vector3.Distance(player.transform.position,npc.transform.position)>30)
            {
                npc.GetComponent<NpcContorl>().SetTransition(Transition.LostPlayer);
            }
        }

        public override void Act(GameObject player, GameObject npc)
        {
            Rigidbody rb = npc.GetComponent<Rigidbody>();
            Vector3 vel = rb.velocity;
            Vector3 moveDir = player.transform.position - npc.transform.position;
            
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,Quaternion.LookRotation(moveDir), 5*Time.deltaTime);
            npc.transform.eulerAngles=new Vector3(0,npc.transform.eulerAngles.y,0);
            vel = moveDir.normalized * 10;
            rb.velocity = vel;
        }
    }
}
