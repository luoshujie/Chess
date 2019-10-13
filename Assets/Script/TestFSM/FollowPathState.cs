using UnityEngine;

namespace Script.FSM
{
    public class FollowPathState : StateBase
    {

        private int currentWayPoint;
        private Transform[] wayPoint;

        public FollowPathState(Transform[] wp)
        {
            wayPoint = wp;
            currentWayPoint = 0;
            stateID = StateID.FollowingPath;
        }
        public override void CheckTransition(GameObject player, GameObject npc)
        {
            //本函数一旦被调用，将会返回以参数1为原点和参数2为半径的球体内“满足一定条件”的碰撞体集合
            Collider[] colliders = Physics.OverlapSphere(npc.transform.position, 5f);
            if (colliders.Length <= 0) return;
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.CompareTag("Player"))
                {
                    npc.GetComponent<NpcContorl>().SetTransition(Transition.SamPlayer);
                }
            }
        }

        public override void Act(GameObject player, GameObject npc)
        {
            Rigidbody rb = npc.GetComponent<Rigidbody>();
            Vector3 vel = rb.velocity;
            Vector3 moveDir = wayPoint[currentWayPoint].position-npc.transform.position;
            if (moveDir.magnitude<1)
            {
                currentWayPoint++;
                if (currentWayPoint>=wayPoint.Length)
                {
                    currentWayPoint = 0;
                }
            }
            else
            {
                vel = moveDir.normalized * 10;
                npc.transform.rotation=Quaternion.Slerp(npc.transform.rotation,Quaternion.LookRotation(moveDir),5*Time.deltaTime );
                npc.transform.eulerAngles=new Vector3(0,npc.transform.eulerAngles.y,0);
            }

            rb.velocity = vel;
        }
    }
}
